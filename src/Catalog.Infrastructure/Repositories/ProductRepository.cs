using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public AppDbContext Context
        {
            get
            {
                return (AppDbContext)_context;
            }
        }

        public async Task<IEnumerable<Product>> GetPagedProducts(PaginationParameters parameters)
        {
            return await Context.Products
                .OrderBy(p => p.Name)
                .Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize)
                .ToListAsync();
        }
    }
}
