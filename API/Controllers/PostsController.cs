using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        // Private readonly fields for the repositories
        private readonly IGenericRepository<Post> _postRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Step> _stepRepo;
        private readonly IMapper _mapper;

        /* 
        The repositories are obtained via the dependency injection container and set to the local fields, the types passed in to the generic repositories
        allow for those repositories to then be used to retrieve information of that type
        */
        public PostsController(
            IGenericRepository<Post> postRepo,
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<Step> stepRepo,
            IMapper mapper)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _stepRepo = stepRepo;
            _mapper = mapper;
        }

        /* 
        All routes are async and return non blocking tasks to allow program to continue operation while waiting for tasks to complete 
        Some routes use automapper to map returned values to DTOs
        */

        // Route to retrieve posts from repository and return an object containg pagination information as well as requested data
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<PostToReturnDto>>> GetPosts([FromQuery] PostSpecParams postParams)
        {
            var spec = new PostsWithCategorySpecification(postParams);
            var countSpec = new PostWithFiltersSpecification(postParams);

            var posts = await _postRepo.ListAsync(spec);
            var totalItems = await _postRepo.CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostToReturnDto>>(posts);

            return Ok(new Pagination<PostToReturnDto>(postParams.PageIndex, postParams.PageSize, totalItems, data));
        }

        // Route to retrieve single post from repository and return the post.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostToReturnDto>> GetPost(int id)
        {
            var spec = new PostsWithCategorySpecification(id);
            var post = await _postRepo.GetEntityWithSpec(spec);

            if (post == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Post, PostToReturnDto>(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            await _postRepo.CreateAsync(post);
            return Ok();
        }

        [HttpPost("steps")]
        public async Task<IActionResult> CreatePostSteps(Step[] steps)
        {
            await _stepRepo.CreateManyAsync(steps);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            await _postRepo.UpdateAsync(id, post);
            return Ok();
        }

        [HttpPut("{id}/steps")]
        public async Task<IActionResult> UpdateSteps(int id, Step[] steps)
        {
            var spec = new StepsSpecification(id);
            await _stepRepo.UpdateManyAsync(spec, steps);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postRepo.DeleteAsync(id);
            return Ok();
        }

        //Route to retrieve the  steps from the repository based on the post id
        [HttpGet("{id}/Steps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<StepToReturnDto>>> GetSteps(int id)
        {
            var spec = new StepsSpecification(id);
            var steps = await _stepRepo.ListAsync(spec);

            if (steps == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<Step>, IReadOnlyList<StepToReturnDto>>(steps));
        }

        // Route to retrieve all categories from the repository and return the categories 
        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            return Ok(await _categoryRepo.ListAllAsync());
        }
    }
}