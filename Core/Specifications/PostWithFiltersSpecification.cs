using Core.Entities;

namespace Core.Specifications
{
    // This specification applies the criteria to the query based on the query params passed from the client
    public class PostWithFiltersSpecification : BaseSpecification<Post>
    {
        public PostWithFiltersSpecification(PostSpecParams postParams)
            : base(x =>
                (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
                (!postParams.CategoryId.HasValue || x.CategoryId == postParams.CategoryId)
            )
        { }
    }
}