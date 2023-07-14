using Core.Entities;

namespace Core.Specifications
{
    // Applies the Criteria function to the query for getting steps that have the provided id
    public class StepsSpecification : BaseSpecification<RecipeStep>
    {
        public StepsSpecification(int id) : base(step => step.RecipePostId == id)
        {

        }
    }
}