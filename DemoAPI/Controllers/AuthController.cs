using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService service, ILogger<AuthController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public ActionResult<AuthResponse> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(
                        new AuthResponse
                        {
                            Success = false,
                            ErrorMessage = "входные данные некорректны"
                        });
                }

                _logger.LogInformation("пользователь {userName} запросил аутентификацию", loginRequest.LoginOrEmail);

                AuthResponse result = _service.Login(loginRequest);
                
                if(!result.Success)
                {
                    _logger.LogWarning("пользователь {username} не смог аутентифицироваться", loginRequest.LoginOrEmail);
                    return Unauthorized(new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "не удалось авторизоваться"
                    });
                }

                _logger.LogInformation("пользователь {username} успешно аутентифицировался", loginRequest.LoginOrEmail);

                //успешный ответ
                var response = new AuthResponse
                {
                    Success = true,
                    Token = result.Token,
                    RefreshToken = result.RefreshToken,
                    ValidTo = result.ValidTo,
                    User = result.User
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError("произошла ошибка аутентификации со стороны сервера {message}", ex.Message);
                return StatusCode(500, new AuthResponse
                {
                    ErrorMessage = "непредвиденная ошибка",
                    Success = false
                });
            }
        }
    }
}
