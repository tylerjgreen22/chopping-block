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

        // Local Criteria
        public Expression<Func<T, bool>> Criteria { get; }

        // List of expressions used for Include statements in the query
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        // Expression to include Order By statements in query, uses private set because only set by helper function in class
        public Expression<Func<T, object>> OrderBy { get; private set; }

        // Expression to include Order By Desc statements in query, uses private set because only set by helper function in class
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        // Three properties for use in paging, check is paging is enabled and indicates how many results to use in page and how many results to skip
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        // Helper method to add include expressions to the Includes list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        // Two helper methods for setting Order By properties to passed in orderByExpression
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
        {
            OrderByDescending = orderByExpression;
        }

        // Helper method for setting Paging props to passed in values
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}