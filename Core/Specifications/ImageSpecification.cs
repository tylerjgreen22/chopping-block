using Core.Entities;

namespace Core.Specifications
{
    // Specification that obtains image by id
    public class ImageSpecification : BaseSpecification<Image>
    {
        public ImageSpecification(string id) : base(image => image.Id == id)
        {

        }
    }
}