namespace Core.Entities
{
    // The recipe step entity, representing the steps in a recipe. Has a foreign key relationship with the Recipe post entity
    public class Step : BaseEntity
    {
        public string Instruction { get; set; }
        public int StepNumber { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}