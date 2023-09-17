using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the like service, which is responsible for creating, deleting and checking likes
    public interface ILikeService
    {
        Task<bool> CheckLike(string postId);
        Task<Like> CreateLike(string postId);
        Task<bool> DeleteLike(string postId);
    }
}