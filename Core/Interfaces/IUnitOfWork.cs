using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the unit of work class, which is responsible for creating instances of repositories and performing work using them
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}