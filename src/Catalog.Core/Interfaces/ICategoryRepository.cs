using Catalog.Core.Entities;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedCollection<Category>> GetPagedCategories(
            PaginationParameters paginationParameters, SearchParameters searchParameters);
        Task<Category?> GetCategoryWithProductsAsync(int id);
    }
}
