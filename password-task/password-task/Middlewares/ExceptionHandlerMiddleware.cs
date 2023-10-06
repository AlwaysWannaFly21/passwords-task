using System.Net;

namespace password_task.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        private const string StandardResponseMessage = "Internal server error";
        private const HttpStatusCode StandardHttpStatusCode = HttpStatusCode.InternalServerError;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex,
                    responseMessage: string.IsNullOrEmpty(ex.Message)
                        ? StandardResponseMessage
                        : ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception,
                                                        HttpStatusCode httpStatusCode = StandardHttpStatusCode, string responseMessage = StandardResponseMessage)
        {
            _logger.LogWarning(exception.Message);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(responseMessage);
        }
    }
}
