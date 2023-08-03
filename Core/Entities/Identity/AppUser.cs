using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    // The app user, extended from IdentityUser used to add additional app specific fields
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}