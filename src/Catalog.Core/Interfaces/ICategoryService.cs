using Catalog.Core.Entities;
using Catalog.Core.Pagination;

namespace Catalog.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<(IEnumerable<Category>, PaginationMetadata)> GetCategories(PaginationParameters parameters);
        Task<Category?> GetCategory(int id);
        Task<Category?> GetCategoryWithProducts(int id);
        Task<Category> AddCategory(Category category);
        Task<Category?> UpdateCategory(int id, Category category);
        Task<bool> RemoveCategory(int id);
    }
}
