using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        // Private readonly fields for the repositories
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        /* 
        The repositories are obtained via the dependency injection container and set to the local fields, the types passed in to the generic repositories
        allow for those repositories to then be used to retrieve information of that type
        */
        public PostsController(
            IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /* 
        All routes are async and return non blocking tasks to allow program to continue operation while waiting for tasks to complete 
        Some routes use automapper to map returned values to DTOs
        */

        // Route to retrieve posts from repository and return an object containg pagination information as well as requested data
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PostToReturnDto>>> GetPosts([FromQuery] PostParams postParams)
        {
            var posts = await _postService.GetPostsAsync(postParams);
            var totalItems = await _postService.GetPostsCountAsync(postParams);

            if (posts == null) return BadRequest(new ApiResponse(400));

            var data = _mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostListToReturnDto>>(posts);

            return Ok(new Pagination<PostListToReturnDto>(postParams.PageIndex, postParams.PageSize, totalItems, data));
        }

        // [HttpGet("user")]
        // public async Task<ActionResult<PostToReturnDto>> GetUserPosts([FromQuery] PostParams postParams)
        // {
        //     var posts = await _postService;
        // }

        // Route to retrieve single post from repository and return the post.
        [HttpGet("{id}")]
        public async Task<ActionResult<PostToReturnDto>> GetPost(string id)
        {
            var post = await _postService.GetPostAsync(id);

            if (post == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Post, PostToReturnDto>(post));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetPostCategories()
        {
            var categories = await _postService.GetPostCategoriesAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto postToCreate)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400, "Invalid post"));
            var post = _mapper.Map<PostDto, Post>(postToCreate);
            var returnPost = await _postService.CreatePostAsync(post);

            if (returnPost == null) return BadRequest(new ApiResponse(400, "Something went wrong while creating your post"));

            return Created($"posts/{returnPost.Id}", _mapper.Map<Post, PostToReturnDto>(post));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostDto postToUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400, "Invalid post"));
            var post = _mapper.Map<PostDto, Post>(postToUpdate);

            var result = await _postService.UpdatePostAsync(id, post);

            if (!result) return BadRequest(new ApiResponse(400, "Something went wrong while updating your post"));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var result = await _postService.DeletePostAsync(id);

            if (!result) return BadRequest(new ApiResponse(400, "Something went wrong while deleting your post"));
            return NoContent();
        }
    }
}