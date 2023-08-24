namespace Core.Entities
{
    public class Like : BaseEntity
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}