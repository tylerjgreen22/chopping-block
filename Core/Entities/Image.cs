namespace Core.Entities
{
    // Image entity which represents images and associates images with users
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}