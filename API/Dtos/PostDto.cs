using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.Dtos
{
    // PostDTO for validating incoming posts
    public class PostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public ICollection<Step> Steps { get; set; }
    }
}