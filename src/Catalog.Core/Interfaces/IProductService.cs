using Catalog.Core.Entities;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Interfaces
{
    public interface IProductService
    {
        Task<PagedCollection<Product>> GetProducts(
            PaginationParameters paginationParameters, SearchParameters searchParameters);
        Task<Product?> GetProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<bool> RemoveProduct(int id);
    }
}
