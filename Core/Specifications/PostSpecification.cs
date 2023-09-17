using Core.Entities;
using Core.Models;

namespace Core.Specifications
{
    // Specification for retreiving posts that match supplied criteria
    public class PostSpecification : BaseSpecification<Post>
    {
        // Constructor that creates a specification that utilizes any critieria passed via query parameters and uses the BaseSpecification helper methods to apply the critieria
        public PostSpecification(PostParams postParams) : base(x =>
                (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
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
                    case "newest":
                        AddOrderByDescending(p => p.CreatedAt);
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

        // Overload for getting posts that match a specific user
        public PostSpecification(PostParams postParams, string userId) : base(x => (string.IsNullOrEmpty(postParams.Search) || x.Title.ToLower().Contains(postParams.Search)) &&
                (string.IsNullOrEmpty(postParams.CategoryId) || x.CategoryId == postParams.CategoryId) && (x.UserId == userId))
        {
            AddInclude(post => post.Category);
            AddOrderBy(post => post.Title);
            ApplyPaging(postParams.PageSize * (postParams.PageIndex - 1), postParams.PageSize);
            if (!string.IsNullOrEmpty(postParams.Sort))
            {
                switch (postParams.Sort)
                {
                    case "newest":
                        AddOrderByDescending(p => p.CreatedAt);
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

        // Overload for getting a single post by id
        public PostSpecification(string id) : base(post => post.Id == id)
        {
            AddInclude(post => post.Category);
            AddInclude(post => post.User);
            AddInclude(post => post.Likes);
            AddInclude(post => post.Steps);
        }
    }
}