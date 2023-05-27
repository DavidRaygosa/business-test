namespace BusinessTest.Application.Middlewares
{
    using BusinessTest.Application.Models;
    using System.Net;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException or AccessViolationException => (int)HttpStatusCode.Unauthorized,
                    HttpRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                ErrorDetail Error = new()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = error?.Message,
                    Stack = error?.StackTrace,
                    Origin = context.Request.Path
                };
                await response.WriteAsync(Error.ToString());
            }
        }
    }
}