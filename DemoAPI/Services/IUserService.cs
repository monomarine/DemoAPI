using DemoAPI.Models.DTO;

namespace DemoAPI.Services
{
    public interface IUserService
    {
        AuthResponse Login(LoginRequest loginRequest);
        AuthResponse Register(CreateUserRequest createUserRequest);
        AuthResponse RefreshToken(string refreshToken);
        bool ValidateToken(string token);
    }
}
