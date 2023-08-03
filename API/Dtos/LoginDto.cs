namespace API.Dtos
{
    // DTO containing relevant information for logging a user in
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}