using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Core.Search;
using Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public AppDbContext Context
        {
            get
            {
                return (AppDbContext)_context;
            }
        }

        public async Task<PagedCollection<Category>> GetPagedCategories(
            PaginationParameters paginationParameters, SearchParameters searchParameters)
        {
            var collection = Context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
                collection = collection.Where(c =>
                    c.Name == searchParameters.Name);

            if (!string.IsNullOrWhiteSpace(searchParameters.SearchQuery))
                collection = collection.Where(c =>
                    EF.Functions.Like(c.Name, $"%{searchParameters.SearchQuery}%"));

            var itemCount = await collection.CountAsync();

            var categories = await collection
                .OrderBy(c => c.Name)
                .Skip(paginationParameters.PageSize * (paginationParameters.PageNumber - 1))
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            return new PagedCollection<Category>(
                categories,
                new PaginationMetadata(itemCount, paginationParameters.PageSize, paginationParameters.PageNumber));
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return await Context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
