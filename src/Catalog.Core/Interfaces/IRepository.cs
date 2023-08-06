using System.Linq.Expressions;

namespace Catalog.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(int id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
