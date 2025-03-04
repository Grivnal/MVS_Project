using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pv311_mvc_project.Models
{
    public class Product : BaseModel<string>
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength]
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [MaxLength(255)]
        public string? Image { get; set; }

        [ForeignKey("Category")]
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}