namespace BusinessTest.Application.Middlewares
{
    using BusinessTest.Business.Authentication;
    using BusinessTest.Business.Roles;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class SecurityHandlerMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly IConfiguration Configuration;
        private IAuthenticationAppService AuthenticationAppService;

        public SecurityHandlerMiddleware(
            IConfiguration configuration,
            RequestDelegate request
        )
        {
            Configuration = configuration;
            Next = request;
        }

        public async Task Invoke(HttpContext context, IAuthenticationAppService authenticationAppService)
        {
            AuthenticationAppService = authenticationAppService;
            ValidateApiSecurity(context);
            await Next(context);
        }
        public void ValidateApiSecurity(HttpContext context)
        {
            // HOME PAGE
            if (context.Request.Path.Equals("/") || context.Request.Path.Equals("/favicon.ico"))
                return;

            // ANONYMOUS ROUTES
            List<string> RoutesAnonymous = new() { "/api/v1/authentication/login" };
            if (RoutesAnonymous.Contains(context.Request.Path))
                return;

            // CREATE CLIENT ROUTE
            if (context.Request.Method == "POST" && context.Request.Path == "/api/v1/client")
                return;

            // HANDLE SECURITY
            string? TokenJWT = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                // VALIDATE TOKEN
                JwtSecurityTokenHandler TokenHandler = new();
                TokenHandler.ValidateToken(TokenJWT, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"]

                }, out SecurityToken validatedToken);
                JwtSecurityToken JwtToken = (JwtSecurityToken)validatedToken;
                string? AuthenticationId = JwtToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                string? PermissionsEncrypted = JwtToken.Claims.FirstOrDefault(x => x.Type == "typ")?.Value;
                if (AuthenticationId is null || PermissionsEncrypted is null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    throw new AccessViolationException($"The current user does not have permissions to use this service");
                }

                // GET PERMISSIONS
                object? EndpointAttribute = context.GetEndpoint()?.Metadata.FirstOrDefault(x => x.GetType().Name == "AuthorizePermissionAttribute");
                string? EndpointPermission = EndpointAttribute is not null ? ((AuthorizePermissionAttribute)EndpointAttribute).Permission : null;
                if (EndpointPermission is not null)
                {
                    Boolean Auth = false;

                    List<string> Permissions = AuthenticationAppService.Validate(context.Request.Headers["Authorization"].FirstOrDefault());
                    Auth = Permissions.Contains(EndpointPermission);

                    if (!Auth)
                        throw new AccessViolationException($"Not enough permissions");
                }
            }
            catch (Exception error)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                throw new AccessViolationException($"The current user does not have permissions to use this service. {error.Message}");
            }
        }
    }
}