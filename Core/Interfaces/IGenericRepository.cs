using Core.Entities;

namespace Core.Interfaces
{
    /* 
    Interface that defines the contract that an implementation of a generic repository must follow. 
    Specifies that the type must be of or dervied from Base Entity
    */
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