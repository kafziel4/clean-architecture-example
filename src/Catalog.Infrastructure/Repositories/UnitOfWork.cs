using Catalog.Core.Interfaces;
using Catalog.Infrastructure.Context;

namespace Catalog.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICategoryRepository> _categories;
        private readonly Lazy<IProductRepository> _products;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _categories = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _products = new Lazy<IProductRepository>(() => new ProductRepository(_context));
        }

        public ICategoryRepository Categories
        {
            get
            {
                return _categories.Value;
            }
        }

        public IProductRepository Products
        {
            get
            {
                return _products.Value;
            }
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
