using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Images
{
    // Interface for the image service, which adds and deletes images from Cloudinary and the local DB
    public interface IImageService
    {
        Task<Image> AddImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string id);
    }
}