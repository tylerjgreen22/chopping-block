using Core.Entities;

namespace Core.Specifications
{
    /* 
    Extends the base specification to provide an implementation of a specification for retrieving a post with the category included,
    also allows for an id to be passed in and used in the criteria
    */
    public class PostsWithCategorySpecification : BaseSpecification<Post>
    {
        // Constructor that creates a specification that utilizes any critieria passed via query parameters and uses the BaseSpecification helper methods to apply the critieria
        public PostsWithCategorySpecification(PostSpecParams postParams) : base(x =>
                (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
                (!postParams.CategoryId.HasValue || x.CategoryId == postParams.CategoryId)
            )
        {
            AddInclude(post => post.Category);
            AddOrderBy(post => post.Title);
            ApplyPaging(postParams.PageSize * (postParams.PageIndex - 1), postParams.PageSize);
        }

        // Overload that takes an int id and uses the BaseSpecification base constructor to utilize the criteria function supplied. Also adds includes
        public PostsWithCategorySpecification(int id) : base(post => post.Id == id)
        {
            AddInclude(post => post.Category);
        }
    }
}