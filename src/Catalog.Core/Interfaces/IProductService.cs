using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<bool> RemoveProduct(int id);
    }
}
