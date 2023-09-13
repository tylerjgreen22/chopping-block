using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IImageAccessor _imageAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IUserAccessor userAccessor, IImageAccessor imageAccessor, IUnitOfWork unitOfWork)
        {
            _userAccessor = userAccessor;
            _imageAccessor = imageAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Image> AddImageAsync(IFormFile file)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return null;

            var imageUploadResult = await _imageAccessor.AddImage(file);

            var image = new Image
            {
                Url = imageUploadResult.Url,
                Id = imageUploadResult.PublicId,
                UserId = user.Id
            };

            _unitOfWork.Repository<Image>().Create(image);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return image;
        }

        public async Task<bool> DeleteImageAsync(string id)
        {
            var result = await _imageAccessor.DeleteImage(id);

            if (result == null) return false;

            var spec = new ImageSpecification(id);
            var image = await _unitOfWork.Repository<Image>().GetEntityWithSpecAsync(spec);
            _unitOfWork.Repository<Image>().Delete(image);
            var success = await _unitOfWork.Complete();
            if (success <= 0) return false;
            return true;
        }
    }
}