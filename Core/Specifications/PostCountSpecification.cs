using Core.Entities;

namespace Core.Specifications
{
    // This specification applies the criteria to the query based on the query params passed from the client. 
    // Mainly used to obtain the count of items matching the filters provided
    public class PostCountSpecification : BaseSpecification<Post>
    {
        public PostCountSpecification(PostParams postParams)
            : base(x =>
                (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
                (string.IsNullOrEmpty(postParams.CategoryId) || x.CategoryId == postParams.CategoryId)
            )
        { }
    }
}