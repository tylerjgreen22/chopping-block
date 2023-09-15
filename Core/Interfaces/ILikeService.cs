using Core.Entities;

namespace Core.Interfaces
{
    public interface ILikeService
    {
        Task<bool> CheckLike(string postId);
        Task<Like> CreateLike(string postId);
        Task<bool> DeleteLike(string postId);
    }
}