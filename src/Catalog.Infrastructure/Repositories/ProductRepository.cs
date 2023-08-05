using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Infrastructure.Context;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
