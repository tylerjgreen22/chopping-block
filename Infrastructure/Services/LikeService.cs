using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    // Like service, responsible for creating, deleting and checking likes
    public class LikeService : ILikeService
    {
        // Injecting unit of work and user accessor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        public LikeService(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
        }

        // Method to check if a user has liked a post
        public async Task<bool> CheckLike(string postId)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return false;

            var spec = new LikeSpecification(postId, user.Id);
            var exisitingLike = await _unitOfWork.Repository<Like>().GetEntityWithSpecAsync(spec);
            if (exisitingLike == null) return false;

            return true;
        }

        // Method to create a like on a post if a user has not already liked the post
        public async Task<Like> CreateLike(string postId)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return null;

            var spec = new LikeSpecification(postId, user.Id);
            var exisitingLike = await _unitOfWork.Repository<Like>().GetEntityWithSpecAsync(spec);
            if (exisitingLike != null) return null;
            var like = new Like { PostId = postId, UserId = user.Id };

            _unitOfWork.Repository<Like>().Create(like);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return like;
        }

        // Delete a like from the db
        public async Task<bool> DeleteLike(string postId)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return false;

            var spec = new LikeSpecification(postId, user.Id);
            var like = await _unitOfWork.Repository<Like>().GetEntityWithSpecAsync(spec);

            if (like == null) return false;

            _unitOfWork.Repository<Like>().Delete(like);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return false;

            return true;
        }
    }
}