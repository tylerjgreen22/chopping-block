using API.Errors;
using Infrastructure.Images;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ImageController : BaseApiController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] IFormFile file)
        {
            var image = await _imageService.AddImageAsync(file);

            if (image == null) return BadRequest(new ApiResponse(400, "Something went wrong uploading your image"));

            return Created($"{image.Url}", image.Url);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _imageService.DeleteImageAsync(id);

            if (!result) return BadRequest(new ApiResponse(400, "Something went wrong deleting your image"));

            return NoContent();
        }
    }
}