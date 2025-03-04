using System.ComponentModel.DataAnnotations;

namespace pv311_mvc_project.Models
{
    public class Category : BaseModel<string>
    {
        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина 100 символів")]
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}