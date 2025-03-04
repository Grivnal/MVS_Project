using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;
using pv311_mvc_project.Repositories.Categories;
using pv311_mvc_project.Repositories.Products;
using pv311_mvc_project.Services.Image;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class ProductController
        (ICategoryRepository categoryRepository, IProductRepository productRepository, IImageService imageService)
        : Controller
    {
        public IActionResult Index()
        {
            var products = productRepository.Products;

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = categoryRepository.GetAll();

            var viewModel = new CreateProductVM
            {
                Categories = categories.Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductVM viewModel)
        {
            string? fileName = null;

            if (viewModel.File != null)
            {
                fileName = await imageService.SaveImageAsync(viewModel.File, Settings.ProductsImagesPath);
            }

            viewModel.Product.Image = fileName;
            viewModel.Product.Id = Guid.NewGuid().ToString();

            await productRepository.CreateAsync(viewModel.Product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.FindByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Product model)
        {
            if(model.Image != null)
            {
                imageService.DeleteImage(Path.Combine(Settings.ProductsImagesPath, model.Image));
            }

            if(model.Id == null)
                return NotFound();

            await productRepository.DeleteAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}