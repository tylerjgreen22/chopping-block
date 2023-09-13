using Core.Entities;

namespace Core.Specifications
{
    public class StepListSpecification : BaseSpecification<Step>
    {
        public StepListSpecification(string id) : base(step => step.PostId == id)
        {

        }
    }
}