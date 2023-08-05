using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetCategoriesWithProductsAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
    }
}
