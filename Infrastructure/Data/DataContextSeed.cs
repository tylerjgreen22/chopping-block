using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    // Seeds the database using the SeedAsync static method. Pulls seed data from the seed data folder and deserializes the data, then adds data to the provided context
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context)
        {
            if (!context.RecipeCategories.Any())
            {
                var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                var categories = JsonSerializer.Deserialize<List<RecipeCategory>>(categoriesData);
                context.RecipeCategories.AddRange(categories);
            }

            if (!context.RecipePosts.Any())
            {
                var postsData = File.ReadAllText("../Infrastructure/Data/SeedData/posts.json");
                var posts = JsonSerializer.Deserialize<List<RecipePost>>(postsData);
                context.RecipePosts.AddRange(posts);
            }

            if (!context.RecipeSteps.Any())
            {
                var stepsData = File.ReadAllText("../Infrastructure/Data/SeedData/steps.json");
                var steps = JsonSerializer.Deserialize<List<RecipeStep>>(stepsData);
                context.RecipeSteps.AddRange(steps);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}