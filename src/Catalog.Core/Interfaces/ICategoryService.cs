using Catalog.Core.Entities;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedCollection<Category>> GetCategories(
            PaginationParameters paginationParameters, SearchParameters searchParameters);
        Task<Category?> GetCategory(int id);
        Task<Category?> GetCategoryWithProducts(int id);
        Task<Category> AddCategory(Category category);
        Task<Category?> UpdateCategory(int id, Category category);
        Task<bool> RemoveCategory(int id);
    }
}
