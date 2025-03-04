using Microsoft.AspNetCore.Mvc.Rendering;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.ViewModels
{
    public class CreateProductVM
    {
        public Product Product { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = [];
        public IFormFile? File { get; set; }
    }
}