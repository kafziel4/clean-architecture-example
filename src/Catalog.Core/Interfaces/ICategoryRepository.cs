using Catalog.Core.Entities;
using Catalog.Core.Pagination;

namespace Catalog.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetPagedCategories(PaginationParameters parameters);
        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
    }
}
