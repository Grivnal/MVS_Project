using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Repositories.Products;
using pv311_mvc_project.Services.Cart;
using pv311_mvc_project.Services.Session;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartController(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = _cartService.GetItems();

            var products = _productRepository
                .GetAll()
                .Where(p => cartItems.Select(i => i.ProductId).Contains(p.Id))
                .ToList();

            var items = products.Select(p => new ProductCartVM
            {
                Product = p,
                Quantity = cartItems.First(i => i.ProductId == p.Id).Quantity
            });

            var viewModel = new CartVM
            {
                Items = items
            };

            var code = HttpContext.Session.Get<string>(Settings.SessionPromoKey);
            if (code != null)
            {
                var codeModel = await _cartService.GetPromoAsync(code);
                if(codeModel != null)
                    viewModel.Discount = codeModel.Discount;

            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.AddToCart(viewModel);
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.RemoveFromCart(viewModel);
            return Ok();
        }

        [HttpPost]
        public IActionResult IncreaseQuantity([FromBody] CartItemVM model)
        {
            var product = _productRepository.FindByIdAsync(model.ProductId).Result;

            if (product == null)
                return BadRequest();

            if(model.Quantity < product.Amount)
            {
                model.Quantity++;
                _cartService.ChangeQuantity(model);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult DecreaseQuantity([FromBody] CartItemVM model)
        {
            if (model.Quantity > 1)
            {
                model.Quantity--;
                _cartService.ChangeQuantity(model);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var cartItems = _cartService.GetItems();
            var products = await _productRepository
                .GetAll()
                .Where(p => cartItems.Select(i => i.ProductId).Contains(p.Id))
                .ToArrayAsync();

            foreach (var item in products)
            {
                item.Amount -= cartItems.First(i => i.ProductId == item.Id).Quantity;
            }

            await _productRepository.UpdateAsync(products);

            HttpContext.Session.Set<IEnumerable<CartItemVM>>(Settings.SessionCartKey, new List<CartItemVM>());

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ActivatePromoCode([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest();

            string oldCode = HttpContext.Session.Get<string>(Settings.SessionPromoKey);
            if (oldCode != null)
                return BadRequest();

            var model = await _cartService.GetPromoAsync(code);

            if(model == null)
                return BadRequest();

            HttpContext.Session.Set(Settings.SessionPromoKey, model.Code);
            return Ok();
        }
    }
}