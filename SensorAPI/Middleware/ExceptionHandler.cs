using SensorAPI.BusinessLayer.Exceptions;

namespace SensorAPI.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate requestDelegate, ILogger<ExceptionHandler> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (BadRequestException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred" });
            }
        }
    }
}
