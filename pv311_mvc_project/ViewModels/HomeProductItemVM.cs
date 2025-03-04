using pv311_mvc_project.Models;

namespace pv311_mvc_project.ViewModels
{
    public class HomeProductItemVM
    {
        public Product Product { get; set; } = new();
        public bool InCart { get; set; } = false;
    }
}