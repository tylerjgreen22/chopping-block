using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Images
{
    public interface IImageAccessor
    {
        Task<ImageUploadResult> AddImage(IFormFile file);
        Task<string> DeleteImage(string publicId);
    }
}