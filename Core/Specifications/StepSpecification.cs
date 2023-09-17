using Core.Entities;

namespace Core.Specifications
{
    // Specification that obtains steps by id
    public class StepSpecification : BaseSpecification<Step>
    {
        public StepSpecification(string id) : base(step => step.PostId == id)
        {

        }
    }
}