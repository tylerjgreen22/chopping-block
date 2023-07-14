using Core.Entities;

namespace Core.Specifications
{
    /* 
    Extends the base specification to provide an implementation of a specification for retrieving a post with the category included,
    also allows for an id to be passed in and used in the criteria
    */
    public class PostsWithCategorySpecification : BaseSpecification<RecipePost>
    {
        // Default constructor that adds the include statement for categories to the include list
        public PostsWithCategorySpecification()
        {
            AddInclude(post => post.RecipeCategory);
        }

        // Overload that takes an int id and uses the BaseSpecification base constructor to utilize the criteria function supplied. Also adds includes
        public PostsWithCategorySpecification(int id) : base(post => post.Id == id)
        {
            AddInclude(post => post.RecipeCategory);
        }
    }
}