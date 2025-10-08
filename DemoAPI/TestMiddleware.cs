using System.Diagnostics;

namespace DemoAPI
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TestMiddleware> _logger;

        public TestMiddleware(RequestDelegate next, ILogger<TestMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch timer = new Stopwatch();
            _logger.LogInformation("начало обработки запроса {StartTime}", DateTime.UtcNow);
            timer.Start();

            await _next.Invoke(context);

            timer.Stop();
            _logger.LogInformation("окончание обработки запроса {EndTime}", DateTime.UtcNow);
            _logger.LogInformation("затраченное время {ellapcedTime} мс", timer.ElapsedMilliseconds);
        }
    }
}
