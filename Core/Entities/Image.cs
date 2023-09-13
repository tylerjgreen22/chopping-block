namespace Core.Entities
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}