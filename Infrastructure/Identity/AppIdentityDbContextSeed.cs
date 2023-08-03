using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    // Seed class for seeding DB with a user
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Tyler",
                    Email = "tyler@gmail.com",
                    UserName = "tyler@gmail.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}