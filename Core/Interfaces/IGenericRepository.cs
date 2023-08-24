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
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> CreateAsync(T entity);
        Task<ICollection<T>> CreateManyAsync(ICollection<T> entities);
        Task<T> UpdateAsync(int id, T entity);
        Task<ICollection<T>> UpdateManyAsync(ISpecification<T> spec, ICollection<T> entities);
        Task<T> DeleteAsync(int id);
    }
}