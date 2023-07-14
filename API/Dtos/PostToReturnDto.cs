namespace API.Dtos
{
    // DTO for the Recipe Post entity, flattens Category and removes steps list and recipe category id
    public class PostToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string RecipeCategory { get; set; }
    }
}