namespace API.Dtos
{
    public class PostListToReturnDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
        public int Likes { get; set; }
        public bool IsLiked { get; set; }
    }
}