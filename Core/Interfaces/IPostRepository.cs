using Core.Entities;

namespace Core.Interfaces
{
    // The interface that defines the contract for the implementation of the post repository to follow
    public interface IPostRepository
    {
        Task<RecipePost> GetRecipePostByIdAsync(int id);
        Task<IReadOnlyList<RecipePost>> GetRecipePostsAsync();
        Task<IReadOnlyList<RecipeCategory>> GetRecipeCategoriesAsync();
        Task<IReadOnlyList<RecipeStep>> GetRecipeStepsByIdAsync(int id);
    }
}