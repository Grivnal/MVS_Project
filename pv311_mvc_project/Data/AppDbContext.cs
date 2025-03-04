using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;
using pv311_mvc_project.Models.Identity;

namespace pv311_mvc_project.Data
{
    public class AppDbContext(DbContextOptions options) 
        : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
    }
}