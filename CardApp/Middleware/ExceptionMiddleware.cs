using CardApp.BLL.Contracts;
using CardApp.BLL.Exceptions;
using System.Net;

namespace CardApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: " + ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            ErrorDetail detail = new ErrorDetail();
            if (exception is CardNotBelongToUserException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                detail.StatusCode = context.Response.StatusCode;
                detail.Message = exception.Message;
            }
            else if (exception is CardNotFoundException)
            {
                detail.StatusCode = (int) HttpStatusCode.NotFound;
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                detail.Message = exception.Message;
            }
            else if (exception is UserNotFoundException)
            {
                detail.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusCode= (int)HttpStatusCode.NotFound;
                detail.Message = exception.Message;
            }
            else
            {
                detail.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                detail.Message = exception.Message;
            }
            await context.Response.WriteAsync(detail.ToString());
        }
      
    }
}
