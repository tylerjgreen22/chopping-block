using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        // Private readonly fields for the repositories
        private readonly IGenericRepository<RecipePost> _recipePostRepo;
        private readonly IGenericRepository<RecipeCategory> _recipeCategoryRepo;
        private readonly IGenericRepository<RecipeStep> _recipeStepRepo;
        private readonly IMapper _mapper;

        /* 
        The repositories are obtained via the dependency injection container and set to the local fields, the types passed in to the generic repositories
        allow for those repositories to then be used to retrieve information of that type
        */
        public PostsController(
            IGenericRepository<RecipePost> recipePostRepo,
            IGenericRepository<RecipeCategory> recipeCategoryRepo,
            IGenericRepository<RecipeStep> recipeStepRepo,
            IMapper mapper)
        {
            _recipePostRepo = recipePostRepo;
            _recipeCategoryRepo = recipeCategoryRepo;
            _recipeStepRepo = recipeStepRepo;
            _mapper = mapper;
        }

        /* 
        All routes are async and return non blocking tasks to allow program to continue operation while waiting for tasks to complete 
        Some routes use automapper to map returned values to DTOs
        */

        // Route to retrieve posts from repository and return list of posts.
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PostToReturnDto>>> GetPosts()
        {
            var spec = new PostsWithCategorySpecification();
            var posts = await _recipePostRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<RecipePost>, IReadOnlyList<PostToReturnDto>>(posts));
        }

        // Route to retrieve single post from repository and return the post.
        [HttpGet("{id}")]
        public async Task<ActionResult<PostToReturnDto>> GetPost(int id)
        {
            var spec = new PostsWithCategorySpecification(id);
            var post = await _recipePostRepo.GetEntityWithSpec(spec);

            return _mapper.Map<RecipePost, PostToReturnDto>(post);
        }

        //Route to retrieve the recipe steps from the repository based on the post id
        [HttpGet("{id}/recipeSteps")]
        public async Task<ActionResult<IReadOnlyList<StepToReturnDto>>> GetRecipeSteps(int id)
        {
            var spec = new StepsSpecification(id);
            var steps = await _recipeStepRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<RecipeStep>, IReadOnlyList<StepToReturnDto>>(steps));
        }

        // Route to retrieve all categories from the repository and return the categories 
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<RecipeCategory>>> GetRecipeCategories()
        {
            return Ok(await _recipeCategoryRepo.ListAllAsync());
        }
    }
}