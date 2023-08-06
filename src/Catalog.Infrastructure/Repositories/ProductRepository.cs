using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Core.Search;
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

        public async Task<PagedCollection<Product>> GetPagedProducts(
            PaginationParameters paginationParameters, SearchParameters searchParameters)
        {
            var collection = Context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
                collection = collection.Where(p =>
                    p.Name == searchParameters.Name);

            if (!string.IsNullOrWhiteSpace(searchParameters.SearchQuery))
                collection = collection.Where(p =>
                    EF.Functions.Like(p.Name, $"%{searchParameters.SearchQuery}%") ||
                    EF.Functions.Like(p.Description, $"%{searchParameters.SearchQuery}%"));

            var itemCount = await collection.CountAsync();

            var products = await collection
                .OrderBy(p => p.Name)
                .Skip(paginationParameters.PageSize * (paginationParameters.PageNumber - 1))
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            return new PagedCollection<Product>(
                products,
                new PaginationMetadata(itemCount, paginationParameters.PageSize, paginationParameters.PageNumber));
        }
    }
}
