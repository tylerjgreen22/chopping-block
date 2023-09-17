using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Images
{
    // Interface for the image accessor, which is responsible for accessing Cloudinary
    public interface IImageAccessor
    {
        Task<ImageUploadResult> AddImage(IFormFile file);
        Task<string> DeleteImage(string publicId);
    }
}