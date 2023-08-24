namespace Core.Entities
{
    // The base entity, which other entities derive from
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}