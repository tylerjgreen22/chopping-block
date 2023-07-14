using System.Linq.Expressions;

namespace Core.Interfaces
{
    // Interface that defines the contract that implementations of specifications must follow. Uses generics to allow for various types to be used
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}