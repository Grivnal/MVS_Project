using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Categories
{
    public class CategoryRepository
        : GenericRepository<Category, string>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => GetAll().Include(c => c.Products);

        public async Task<Category?> FindByNameAsync(string name)
        {
            return await Categories
                .FirstOrDefaultAsync(c =>
                c.Name == null ? false
                : c.Name.ToLower() == name.ToLower());
        }
    }
}