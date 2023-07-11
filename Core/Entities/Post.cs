namespace Core.Entities
{
    // The Post entity, representing the core of the application, the post. 
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}