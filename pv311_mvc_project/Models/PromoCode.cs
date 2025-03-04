using System.ComponentModel.DataAnnotations;

namespace pv311_mvc_project.Models
{
    public class PromoCode
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? Code { get; set; }
        public int Discount { get; set; } = 0;
        public DateTime ExpiredTime { get; set; }
    }
}