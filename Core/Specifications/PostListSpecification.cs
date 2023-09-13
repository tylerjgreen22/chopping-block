using Core.Entities;
using Core.Models;

namespace Core.Specifications
{
    /* 
    Extends the base specification to provide an implementation of a specification for retrieving a post with the category included,
    also allows for an id to be passed in and used in the criteria
    */
    public class PostListSpecification : BaseSpecification<Post>
    {
        // Constructor that creates a specification that utilizes any critieria passed via query parameters and uses the BaseSpecification helper methods to apply the critieria
        public PostListSpecification(PostParams postParams) : base(x =>
                (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
                (string.IsNullOrEmpty(postParams.UserId) || x.UserId == postParams.UserId) &&
                (string.IsNullOrEmpty(postParams.CategoryId) || x.CategoryId == postParams.CategoryId)
            )
        {
            AddInclude(post => post.Category);
            AddInclude(post => post.User);
            AddInclude(post => post.Likes);
            AddOrderBy(post => post.Title);
            ApplyPaging(postParams.PageSize * (postParams.PageIndex - 1), postParams.PageSize);

            if (!string.IsNullOrEmpty(postParams.Sort))
            {
                switch (postParams.Sort)
                {
                    case "likesAsc":
                        AddOrderBy(p => p.Likes.Count());
                        break;
                    case "likesDesc":
                        AddOrderByDescending(p => p.Likes.Count());
                        break;
                    default:
                        AddOrderBy(p => p.Title);
                        break;
                }
            }
        }
    }
}