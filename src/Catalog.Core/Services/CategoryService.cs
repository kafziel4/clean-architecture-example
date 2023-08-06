using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Core.Search;

namespace Catalog.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedCollection<Category>> GetCategories(
            PaginationParameters paginationParameters, SearchParameters searchParameters)
        {
            return await _unitOfWork.Categories.GetPagedCategories(paginationParameters, searchParameters);
        }

        public async Task<Category?> GetCategory(int id)
        {
            return await _unitOfWork.Categories.GetAsync(id);
        }

        public async Task<Category?> GetCategoryWithProducts(int id)
        {
            return await _unitOfWork.Categories.GetCategoryWithProductsAsync(id);
        }

        public async Task<Category> AddCategory(Category category)
        {
            _unitOfWork.Categories.Add(category);
            await _unitOfWork.CommitAsync();
            return category;
        }

        public async Task<Category?> UpdateCategory(int id, Category category)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(id);
            if (categoryEntity is null)
                return null;

            categoryEntity.Update(category.Name, category.ImageUrl);

            await _unitOfWork.CommitAsync();
            return categoryEntity;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(id);
            if (categoryEntity is null)
                return false;

            _unitOfWork.Categories.Remove(categoryEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
