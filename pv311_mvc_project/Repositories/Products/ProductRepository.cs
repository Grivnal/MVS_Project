using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Products
{
    public class ProductRepository
        : GenericRepository<Product, string>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> Products { get => GetAll().Include(p => p.Category); }

        public IQueryable<Product> GetByCategory(string category)
        {
            return Products.Where(p => 
            p.Category == null ? false 
            : p.Category.Name == null ? false 
            : p.Category.Name.ToLower() == category.ToLower());
        }
    }
}