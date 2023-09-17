namespace Core.Entities
{
    /* 
    The Post entity, representing the core of the application, the post. Includes the category, steps, user and likes that are attributed to the post
    */
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Step> Steps { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Like> Likes { get; set; }
        public bool IsLiked { get; set; } = false;
    }
}