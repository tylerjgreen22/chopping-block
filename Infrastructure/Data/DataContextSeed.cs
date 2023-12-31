using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    // Seeds the database using the SeedAsync static method. Pulls seed data from the seed data folder and deserializes the data, then adds data to the provided context
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, UserManager<AppUser> userManager)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser> {
                    new AppUser {
                        Id = "1678a7d9-72cf-40d7-9eaa-86c5141c8b21",
                        Email = "tyler@test.com",
                        UserName = "Tyler"
                    },
                    new AppUser {
                        Id = "ff186661-6fc6-4072-87c8-6e24468a906f",
                        Email = "mark@test.com",
                        UserName = "Mark"
                    },
                    new AppUser {
                        Id = "307efb87-ccbb-4c2f-a4fb-a89270c670fb",
                        Email = "john@gmail.com",
                        UserName = "John"
                    }

                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText(path + @"/Data/SeedData/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                context.Categories.AddRange(categories);
            }

            if (!context.Posts.Any())
            {
                var postsData = File.ReadAllText(path + @"/Data/SeedData/posts.json");
                var posts = JsonSerializer.Deserialize<List<Post>>(postsData);
                context.Posts.AddRange(posts);
            }

            if (!context.Steps.Any())
            {
                var stepsData = File.ReadAllText(path + @"/Data/SeedData/steps.json");
                var steps = JsonSerializer.Deserialize<List<Step>>(stepsData);
                foreach (var step in steps)
                {
                    step.Id = Guid.NewGuid().ToString();
                }
                context.Steps.AddRange(steps);
            }

            if (!context.Likes.Any())
            {
                var likesData = File.ReadAllText(path + @"/Data/SeedData/likes.json");
                var likes = JsonSerializer.Deserialize<List<Like>>(likesData);
                context.Likes.AddRange(likes);
            }

            if (!context.Images.Any())
            {
                var imagesData = File.ReadAllText(path + @"/Data/SeedData/images.json");
                var images = JsonSerializer.Deserialize<List<Image>>(imagesData);
                context.Images.AddRange(images);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}