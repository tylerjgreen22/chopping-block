using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
    This class is the implementation of the generic repository specified by the interface. It can take types of Base Entity
    The generic repository pattern is used to create a repository that can be used with many different entities, whose methods are
    not dependent on the entity being used. To achieve this effectively, this pattern is used in conjunction with the specification pattern.
    Essentially, queries are created using the specification provided, which are then called within this repository. The SpecificationEvaluator is
    used to evaluate the provided specification, and construct the query using the information provided
    */
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        // Private readonly field for the data context
        private readonly DataContext _context;

        // Data context is retrieved from the dependency injection container and used to interact with the database
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        // All methods return async non blocking Tasks to free up thread pool to continue execution of the program

        // Retrieves a list of items from the database
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // Retrieves a list of items from the database using the provided specification
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // Retrieves an item from the database using the provided specification
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsSplitQuery().FirstOrDefaultAsync();
        }

        // Counts the total items retrieved based on filter specification
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        // Adds entity to the db set of the entity type
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        // Updates entity in the db set
        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // Removes entity from the db set
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        // Helper function that applies the specifications to a query on a database and returns the resulting query
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }


    }
}