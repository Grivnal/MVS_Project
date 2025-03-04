using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Products
{
    public interface IProductRepository
        : IGenericRepository<Product, string>
    {
        IQueryable<Product> Products { get; }
        IQueryable<Product> GetByCategory(string category);
    }
}