namespace DemoAPI.Models.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ValidTo { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
