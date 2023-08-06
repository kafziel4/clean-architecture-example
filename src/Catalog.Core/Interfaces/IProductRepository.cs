using Catalog.Core.Entities;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedCollection<Product>> GetPagedProducts(
            PaginationParameters paginationParameters, SearchParameters searchParameters);
    }
}
