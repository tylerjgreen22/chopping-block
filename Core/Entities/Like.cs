namespace Core.Entities
{
    // Like entity which is a combination of a user and a post, so a user can only like a post once
    public class Like : BaseEntity
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public string PostId { get; set; }
    }
}