using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the generic repository, responsible for CRUD operations on a generic type, utilizes specification pattern
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}