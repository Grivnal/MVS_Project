using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;
using pv311_mvc_project.Services.Session;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Services.Cart
{
    public class CartService : ICartService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public CartService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public void AddToCart(CartItemVM viewModel)
        {
            var context = _httpContextAccessor.HttpContext;
            if(context == null)
                return;
            var session = context.Session;

            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);
            var list = items == null ? new List<CartItemVM>() : items.ToList();

            list.Add(viewModel);
            session.Set<IEnumerable<CartItemVM>>(Settings.SessionCartKey, list);
        }

        public IEnumerable<CartItemVM> GetItems()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return new List<CartItemVM>();
            var session = context.Session;

            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);

            return items ?? new List<CartItemVM>();
        }

        public void SetItems(IEnumerable<CartItemVM> items)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;
            var session = context.Session;

            session.Set(Settings.SessionCartKey, items);
        }

        public void RemoveFromCart(CartItemVM viewModel)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;
            var session = context.Session;

            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);
            items ??= new List<CartItemVM>();
            items = items.Where(i => i.ProductId != viewModel.ProductId);
            
            session.Set(Settings.SessionCartKey, items);
        }

        public static int GetCount(HttpContext? context)
        {
            if (context == null)
                return 0;
            var session = context.Session;
            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);
            return items == null ? 0 : items.Count();
        }

        public void ChangeQuantity(CartItemVM viewModel)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(i => i.ProductId == viewModel.ProductId);

            if (item != null)
            {
                item.Quantity = viewModel.Quantity;
                SetItems(items);
            }
        }

        public async Task<PromoCode?> GetPromoAsync(string code)
        {
            return await _context.PromoCodes.FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}