using Catalog.Core.Entities;
using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedCollection<Product>> GetProducts(
            PaginationParameters paginationParameters, SearchParameters searchParameters)
        {
            return await _unitOfWork.Products.GetPagedProducts(paginationParameters, searchParameters);
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _unitOfWork.Products.GetAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await ValidateCategory(product);

            _unitOfWork.Products.Add(product);
            await _unitOfWork.CommitAsync();
            return product;
        }

        public async Task<Product?> UpdateProduct(int id, Product product)
        {
            var productEntity = await _unitOfWork.Products.GetAsync(id);
            if (productEntity is null)
                return null;

            await ValidateCategory(product);

            productEntity.Update(
                product.Name,
                product.Description,
                product.Price,
                product.ImageUrl,
                product.Stock,
                product.RegistrationDate,
                product.CategoryId);

            await _unitOfWork.CommitAsync();
            return productEntity;
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var productEntity = await _unitOfWork.Products.GetAsync(id);
            if (productEntity is null)
                return false;

            _unitOfWork.Products.Remove(productEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        private async Task ValidateCategory(Product product)
        {
            var categoryExists = await _unitOfWork.Categories
                .ExistsAsync(c => c.Id == product.CategoryId);

            ValidationException.When(!categoryExists,
                "Category does not exist.");
        }
    }
}
