using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        // Private readonly field for the repository
        private readonly IPostRepository _repository;

        /* 
        The repository is obtained via the dependency injection container and instatiated to the local private variable _repository
        _repository is used to interact with the implementation of the repository, which in turn is used to interact with the database
        */
        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        /* All routes are async and return non blocking tasks to allow program to continue operation while waiting for tasks to complete */

        // Route to retrieve posts from repository and return list of posts.
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RecipePost>>> GetPosts()
        {
            return Ok(await _repository.GetRecipePostsAsync());
        }

        // Route to retrieve single post from repository and return the post.
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipePost>> GetPost(int id)
        {
            return await _repository.GetRecipePostByIdAsync(id);
        }

        // Route to retrieve the recipe steps from the repository based on the post id
        [HttpGet("{id}/recipeSteps")]
        public async Task<ActionResult<IReadOnlyList<RecipeStep>>> GetRecipeSteps(int id)
        {
            return Ok(await _repository.GetRecipeStepsByIdAsync(id));
        }

        // Route to retrieve all categories from the repository and return the categories 
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<RecipeCategory>>> GetRecipeCategories()
        {
            return Ok(await _repository.GetRecipeCategoriesAsync());
        }
    }
}