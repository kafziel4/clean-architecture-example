using Catalog.Core.Entities;
using Catalog.Core.Pagination;

namespace Catalog.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetPagedProducts(PaginationParameters parameters);
    }
}
