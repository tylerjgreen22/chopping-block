namespace Core.Entities
{
    // The Step entity, representing the steps in a recipe. Includes the post that the steps belong too
    public class Step : BaseEntity
    {
        public string Instruction { get; set; }
        public int StepNumber { get; set; }
        public Post Post { get; set; }
        public string PostId { get; set; }
    }
}