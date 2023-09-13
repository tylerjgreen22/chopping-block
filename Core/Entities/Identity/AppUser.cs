using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    // The app user, extended from IdentityUser used to add additional app specific fields
    public class AppUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}