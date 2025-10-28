using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DemoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly JwtConfiguration _jwtSett;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<JwtConfiguration> jwtConf,
            IUserRepository userRepo)
        {
            _jwtSett = jwtConf.Value;
            _userRepository = userRepo;
        }

        public AuthResponse Login(LoginRequest loginRequest)
        {
            try
            {
                var user = _userRepository.ExistUser(loginRequest.LoginOrEmail);
                if (user == null)
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Такого пользователя не существует"
                    };

                if (user.PasswordHash != GetHashPassword(loginRequest.Password))
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Пароль не соответствует"
                    };
                else
                {
                    var token = GenerateJwtToken(user);
                    var refrashToken = GenerateRefreshToken();

                    return new AuthResponse
                    {
                        Success = true,
                        Token = token,
                        RefreshToken = refrashToken,
                        ValidTo = DateTime.UtcNow
                        .AddMinutes(_jwtSett.ExpirateAtInMinutes),
                        User = new UserDTO
                        {
                            Id = user.Id,
                            Login = user.Login,
                            Email = user.Email
                        }
                    };
                }
            }
            catch(Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    ErrorMessage = $"Некорретные данные + {ex.Message}"
                };
            }
        }

        public AuthResponse RefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public AuthResponse Register(CreateUserRequest createUserRequest)
        {
            try
            {


                var user1 = _userRepository.ExistUser(createUserRequest.Email);
                var user2 = _userRepository.ExistUser(createUserRequest.Login);

                if (user1 != null || user2 != null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Такой пользователь уже существует"
                    };
                }
                else
                {
                    User newUser = new User
                    {
                        Login = createUserRequest.Login,
                        Email = createUserRequest.Email,
                        PasswordHash = GetHashPassword(createUserRequest.Password),
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    var addedUser = _userRepository.AddUser(newUser);

                    return new AuthResponse
                    {
                        Success = true,
                        Token = GenerateJwtToken(addedUser),
                        RefreshToken = GenerateRefreshToken(),
                        ValidTo = DateTime.UtcNow
                            .AddMinutes(_jwtSett.ExpirateAtInMinutes),
                        User = new UserDTO
                        {
                            Id = addedUser.Id,
                            Login = addedUser.Login,
                            Email = addedUser.Email
                        }
                    };

                }
            }
            catch(Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSett.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSett.ExpirateAtInMinutes),
                Issuer = _jwtSett.Issuer,
                Audience = _jwtSett.Audience,
                SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256Signature
                                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rnd =  System.Security.Cryptography.RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private string GetHashPassword(string password)
        {
            byte[] bytepass = Encoding.ASCII.GetBytes(password);
            var hashBytes = SHA256.HashData(bytepass);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
