using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
    The implementation of the post repository, 
    implements the IPostRepository interface and provides concrete implementations of the methods specified in the interface 
    */
    public class PostRepository : IPostRepository
    {
        // Private readonly field representing the data context
        private readonly DataContext _context;

        /* 
        Pulling the data context from the dependency injection container and instantiating it to the private field _context
        _context is used to interact with the database
        */
        public PostRepository(DataContext context)
        {
            _context = context;
        }

        // All methods return async non blocking Tasks to free up thread pool to continue execution of the program

        // Retrieves all posts from the database
        public async Task<IReadOnlyList<RecipePost>> GetRecipePostsAsync()
        {
            return await _context.RecipePosts.ToListAsync();
        }

        // Retrieves single post from database using Id. Also inlcudes the post category
        public async Task<RecipePost> GetRecipePostByIdAsync(int id)
        {
            return await _context.RecipePosts.Include(post => post.RecipeCategory).FirstOrDefaultAsync(post => post.Id == id);
        }

        // Retrieves all categories from the database
        public async Task<IReadOnlyList<RecipeCategory>> GetRecipeCategoriesAsync()
        {
            return await _context.RecipeCategories.ToListAsync();
        }

        // Retrives recipe steps based on the provided id of the recipe post
        public async Task<IReadOnlyList<RecipeStep>> GetRecipeStepsByIdAsync(int id)
        {
            return await _context.RecipeSteps.Where(step => step.RecipePostId == id).ToListAsync();
        }
    }
}