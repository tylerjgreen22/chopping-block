using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    // DTO containing relevant information to register a user
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric, 6 chars long")]
        public string Password { get; set; }
    }
}