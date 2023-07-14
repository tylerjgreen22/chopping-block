namespace Core.Entities
{
    /* 
    The RecipePost entity, representing the core of the application, the recipe post. 
    Has a foreign key relationship with the recipe category. 
    */
    public class RecipePost : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public RecipeCategory RecipeCategory { get; set; }
        public int RecipeCategoryId { get; set; }
        public List<RecipeStep> RecipeSteps { get; set; }
    }
}