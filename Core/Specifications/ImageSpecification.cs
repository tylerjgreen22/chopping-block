using Core.Entities;

namespace Core.Specifications
{
    public class ImageSpecification : BaseSpecification<Image>
    {
        public ImageSpecification(string id) : base(image => image.Id == id)
        {

        }
    }
}