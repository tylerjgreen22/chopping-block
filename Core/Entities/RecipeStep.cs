namespace Core.Entities
{
    // The recipe step entity, representing the steps in a recipe. Has a foreign key relationship with the Recipe post entity
    public class RecipeStep : BaseEntity
    {
        public string? Step { get; set; }
        public int StepNumber { get; set; }
        public RecipePost? RecipePost { get; set; }
        public int RecipePostId { get; set; }
    }
}