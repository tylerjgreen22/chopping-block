using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Images
{
    public interface IImageService
    {
        Task<Image> AddImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string id);
    }
}