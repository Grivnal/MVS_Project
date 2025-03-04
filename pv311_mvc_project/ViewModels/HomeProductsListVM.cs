using pv311_mvc_project.Models;

namespace pv311_mvc_project.ViewModels
{
    public class HomeProductsListVM
    {
        public IEnumerable<HomeProductItemVM> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
        public int Page { get; set; } = 1;
        public int PagesCount { get; set; }
        public string Category { get; set; } = "";
    }
}