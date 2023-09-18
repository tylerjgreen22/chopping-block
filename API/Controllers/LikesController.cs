using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controller for interacting likes
    public class LikesController : BaseApiController
    {
        // Injecting like service
        private readonly ILikeService _likeService;
        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        // Check if a user has liked a post
        [HttpGet("{postId}")]
        public async Task<IActionResult> CheckLike(string postId)
        {
            var like = await _likeService.CheckLike(postId);

            return Ok(like);
        }

        // Create a like using post id
        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateLike(string postId)
        {
            var like = await _likeService.CreateLike(postId);

            if (like == null) return BadRequest(new ApiResponse(400, "Problem liking post"));

            return StatusCode(201);
        }

        // Delete a like by post id
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteLike(string postId)
        {
            var result = await _likeService.DeleteLike(postId);

            if (result == false) return BadRequest(new ApiResponse(400, "Problem unliking post"));

            return NoContent();
        }
    }
}