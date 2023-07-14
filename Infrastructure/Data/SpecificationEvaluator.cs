using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
    This class is responsible for building the queryable using the specification provided. It can take a type that is of BaseEntity, and it has a 
    static method that builds the query by taking the input query and appying the criteria and includes statements provided by the spec 
    */
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            // Applying critieria to query if supplied
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // Applying includes to query
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}