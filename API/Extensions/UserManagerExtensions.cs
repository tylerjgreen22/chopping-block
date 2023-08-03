using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    // Extension class for extending the user manager
    public static class UserManagerExtensions
    {
        // Searching for user by email
        public static async Task<AppUser> FindUserByEmailClaimsPrinciple(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}