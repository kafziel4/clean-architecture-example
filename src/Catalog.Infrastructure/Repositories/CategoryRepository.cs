using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
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

        public async Task<IEnumerable<Category>> GetPagedCategories(PaginationParameters parameters)
        {
            return await Context.Categories
                .OrderBy(c => c.Name)
                .Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return await Context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await Context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
