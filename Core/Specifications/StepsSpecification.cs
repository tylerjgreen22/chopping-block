using Core.Entities;

namespace Core.Specifications
{
    // Applies the Criteria function to the query for getting steps that have the provided id
    public class StepsSpecification : BaseSpecification<Step>
    {
        public StepsSpecification(int id) : base(step => step.PostId == id)
        {

        }
    }
}