using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace pv311_mvc_project.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(255)]
        public string? FirstName { get; set; }
        [MaxLength(255)]
        public string? LastName { get; set; }
        [Range(0, 200)]
        public int Age { get; set; }
    }
}