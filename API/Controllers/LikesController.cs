using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly ILikeService _likeService;
        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateLike(string postId)
        {
            var like = await _likeService.CreateLike(postId);

            if (like == null) return BadRequest(new ApiResponse(400, "Problem liking post"));

            return StatusCode(201);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteLike(string postId)
        {
            var result = await _likeService.DeleteLike(postId);

            if (result == false) return BadRequest(new ApiResponse(400, "Problem unliking post"));
            return NoContent();
        }
    }
}