namespace API.Dtos
{
    // DTO for the user, contains the relevant information to return and receive from the client
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}