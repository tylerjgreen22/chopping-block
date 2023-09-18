namespace API.Dtos
{
    // Post to return DTO, returns relevant information including user, likes and steps
    public class PostToReturnDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
        public int Likes { get; set; }
        public ICollection<StepToReturnDto> Steps { get; set; }
    }
}