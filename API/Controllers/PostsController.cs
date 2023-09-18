using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controller for interacting with posts
    public class PostsController : BaseApiController
    {
        // Injecting post service and auto mapper
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(
            IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        // Get paginated posts matching provided query params (postParams) as well as the total posts matching that set of query params
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PostToReturnDto>>> GetPosts([FromQuery] PostParams postParams)
        {
            var posts = await _postService.GetPostsAsync(postParams);
            var totalItems = await _postService.GetPostsCountAsync(postParams);

            if (posts == null) return BadRequest(new ApiResponse(400));

            var data = _mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostListToReturnDto>>(posts);

            return Ok(new Pagination<PostListToReturnDto>(postParams.PageIndex, postParams.PageSize, totalItems, data));
        }

        // Get a post by id, will use cache if possible
        [Cached(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostToReturnDto>> GetPost(string id)
        {
            var post = await _postService.GetPostAsync(id);

            if (post == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Post, PostToReturnDto>(post));
        }

        // Get categories, will use cache if possible
        [Cached(600)]
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetPostCategories()
        {
            var categories = await _postService.GetPostCategoriesAsync();

            return Ok(categories);
        }

        // Create a post if post is valid, requires user to be authorized
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto postToCreate)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400, "Invalid post"));
            var post = _mapper.Map<PostDto, Post>(postToCreate);
            var returnPost = await _postService.CreatePostAsync(post);

            if (returnPost == null) return BadRequest(new ApiResponse(400, "Something went wrong while creating your post"));

            return Created($"posts/{returnPost.Id}", _mapper.Map<Post, PostToReturnDto>(post));
        }

        // Update a post if post is valid, requires user to be authorized against custom auth policy
        [Authorize(Policy = "IsOwner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostDto postToUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400, "Invalid post"));
            var post = _mapper.Map<PostDto, Post>(postToUpdate);

            var result = await _postService.UpdatePostAsync(id, post);

            if (!result) return BadRequest(new ApiResponse(400, "Something went wrong while updating your post"));
            return NoContent();
        }

        // Delete a post, requires user to be authorized against custom auth policy
        [Authorize(Policy = "IsOwner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var result = await _postService.DeletePostAsync(id);

            if (!result) return BadRequest(new ApiResponse(400, "Something went wrong while deleting your post"));
            return NoContent();
        }
    }
}