using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    // Unit of work used to manage database transactions and data access
    public class UnitOfWork : IUnitOfWork
    {
        // Obtainting db context via dependency injection
        private readonly DataContext _context;
        private Hashtable _repositories;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        // Method that is called when transactions are complete, saving the changes
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        // Disposes of resources held by context
        public void Dispose()
        {
            _context.Dispose();
        }

        // Creates repositories and adds them to the hashtable if they don't already exist
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repositories ??= new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}