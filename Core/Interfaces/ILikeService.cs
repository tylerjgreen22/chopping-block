using Core.Entities;

namespace Core.Interfaces
{
    public interface ILikeService
    {
        Task<Like> CreateLike(string postId);
        Task<bool> DeleteLike(string postId);
    }
}