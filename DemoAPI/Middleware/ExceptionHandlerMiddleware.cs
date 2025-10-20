using System.Net;
using System.Text.Json;

namespace DemoAPI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<ExceptionHandlerMiddleware> _logger; 

        public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public void Invoke(HttpContext context) 
        {
            try
            {
                _next(context); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошло необработанное исключение: {Message}", ex.Message);
                HandleException(context, ex); 
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json"; 
            var response = new ApiResponse<object>(
                message:"Произошло необработанное исключение", 
                errorCode:"INTERNAL_ERROR"); 

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError; //500

            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response.Message = "The requested resource was not found.";
                    response.ErrorCode = "NOT_FOUND";
                    break;

                case ArgumentException:
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    response.ErrorCode = "BAD_REQUEST";
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    response.Message = "Access denied";
                    response.ErrorCode = "UNAUTHORIZED";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            context.Response.WriteAsync(json);
        }
    }
}