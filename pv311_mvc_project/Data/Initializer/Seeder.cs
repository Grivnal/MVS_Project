using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;
using pv311_mvc_project.Models.Identity;

namespace pv311_mvc_project.Data.Initializer
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.Migrate();

                // Roles and Users
                if(!roleManger.Roles.Any())
                {
                    var adminRole = new IdentityRole { Name = Settings.RoleAdmin };
                    var userRole = new IdentityRole { Name = Settings.RoleUser };

                    roleManger.CreateAsync(adminRole).Wait();
                    roleManger.CreateAsync(userRole).Wait();
                }

                if(!userManger.Users.Any())
                {
                    var admin = new AppUser 
                    {
                        Email = "admin@gmail.com",
                        UserName = "admin",
                        FirstName = "admin",
                        LastName = "admin",
                        EmailConfirmed = true
                    };

                    var user = new AppUser
                    {
                        Email = "user@gmail.com",
                        UserName = "user",
                        FirstName = "user",
                        LastName = "user",
                        EmailConfirmed = true
                    };

                    userManger.CreateAsync(admin, "qwerty").Wait();
                    userManger.CreateAsync(user, "qwerty").Wait();

                    userManger.AddToRoleAsync(admin, Settings.RoleAdmin).Wait();
                    userManger.AddToRoleAsync(user, Settings.RoleUser).Wait();
                }

                // Categories and Products
                if (!context.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Процесори" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Материнські плати" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Блоки живлення" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Оперативна пам'ять" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Відеокарти" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Накопичувачі" }
                    };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();

                    var products = new List<Product>
                    {
                        new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte GeForce RTX 4060 Gaming OC 8192MB", Price = 15799, Amount = 5,CategoryId = categories[4].Id },
                        new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte B550M AORUS ELITE AX", Price = 4699, Amount = 10, CategoryId = categories[1].Id }
                    };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }
}