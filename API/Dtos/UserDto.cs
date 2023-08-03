using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    // DTO for the user, contains the relevant information to return and receive from the client. Also provides validation with data annotations
    public class UserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric, 6 chars long")]
        public string Token { get; set; }
    }
}