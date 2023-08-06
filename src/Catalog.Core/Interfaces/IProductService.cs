using Catalog.Core.Entities;
using Catalog.Core.Pagination;

namespace Catalog.Core.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product>, PaginationMetadata)> GetProducts(PaginationParameters parameters);
        Task<Product?> GetProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<bool> RemoveProduct(int id);
    }
}
