using Core.Entities;

namespace Core.Specifications
{
    /* 
    Extends the base specification to provide an implementation of a specification for retrieving a post with the category included,
    also allows for an id to be passed in and used in the criteria
    */
    public class PostDetailSpecification : BaseSpecification<Post>
    {
        // Constructor that takes an int id and uses the BaseSpecification base constructor to utilize the criteria function supplied. Also adds includes
        public PostDetailSpecification(string id) : base(post => post.Id == id)
        {
            AddInclude(post => post.Category);
            AddInclude(post => post.User);
            AddInclude(post => post.Likes);
            AddInclude(post => post.Steps);
        }
    }
}