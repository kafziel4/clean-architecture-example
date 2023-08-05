namespace Catalog.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        Task<int> CommitAsync();
    }
}
