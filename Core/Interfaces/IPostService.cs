using Core.Entities;
using Core.Models;

namespace Core.Interfaces
{
    public interface IPostService
    {
        Task<IReadOnlyList<Post>> GetPostsAsync(PostParams postParams);
        Task<int> GetPostsCountAsync(PostParams postParams);
        Task<Post> GetPostAsync(string Id);
        Task<IReadOnlyList<Category>> GetPostCategoriesAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(string Id, Post post);
        Task<bool> DeletePostAsync(string Id);
    }
}