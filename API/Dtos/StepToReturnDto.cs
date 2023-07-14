namespace API.Dtos
{
    // DTO for the step enitity, removes the recipe post id and entity
    public class StepToReturnDto
    {
        public int Id { get; set; }
        public string? Step { get; set; }
        public int StepNumber { get; set; }
    }
}