namespace API.Dtos
{
    // DTO for the step enitity, removes the post id and entity
    public class StepToReturnDto
    {
        public string Id { get; set; }
        public string Instruction { get; set; }
        public int StepNumber { get; set; }
    }
}