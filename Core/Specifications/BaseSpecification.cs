using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    // Base implementation of the specification interface that provides methods for building a queryable using criteria and includes statements
    public class BaseSpecification<T> : ISpecification<T>
    {
        // Default constructor
        public BaseSpecification() { }

        // Constructor for setting local criteria to the criteria passed in
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        // Local Criteria (function that returns a bool in this case)
        public Expression<Func<T, bool>> Criteria { get; }

        // List of expressions used for Include statements in the query
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        // Helper method to add include expressions to the Includes list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}