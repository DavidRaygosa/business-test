namespace BusinessTest.Application.Extensions
{
    using BusinessTest.Application.Middlewares;
    public static class ApplicationExtensions
    {
        public static void UseSecurityMiddleware(this IApplicationBuilder app) => app.UseMiddleware<SecurityHandlerMiddleware>();
    }
}