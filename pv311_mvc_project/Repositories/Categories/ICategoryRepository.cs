using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Categories
{
    public interface ICategoryRepository 
        : IGenericRepository<Category, string>
    {
        IQueryable<Category> Categories { get; }
        Task<Category?> FindByNameAsync(string name);
    }
}