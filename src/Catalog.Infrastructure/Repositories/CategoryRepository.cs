using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
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

        public async Task<Category?> GetCategoriesWithProductsAsync(int id)
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
