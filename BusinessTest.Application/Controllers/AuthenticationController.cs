namespace BusinessTest.Application.Controllers
{
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;
    using BusinessTest.Business.Authentication;
    using BusinessTest.Business.Authentication.Models;
    using BusinessTest.Application.Models;
    using Microsoft.AspNetCore.Authorization;

    public class AuthenticationController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/v1/authentication/login", AuthenticationLogin).WithName("AuthenticationLogin").WithTags("Authentication").WithMetadata(new SwaggerOperationAttribute("Login", "Login")).Produces<string>(StatusCodes.Status200OK);
            app.MapGet("api/v1/authentication/validate", AuthenticationValidate).WithName("AuthenticationValidate").WithTags("Authentication").WithMetadata(new SwaggerOperationAttribute("Permissions", "Permissions")).Produces<List<string>>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Authorization_Validate)]
        private IResult AuthenticationValidate(IAuthenticationAppService service, [FromHeader] string? Authorization)
        {
            List<string> permissions = service.Validate(Authorization);
            return Results.Ok(permissions);
        }

        [AllowAnonymous]
        private IResult AuthenticationLogin(IAuthenticationAppService service, [FromBody] AuthenticationLoginRequestModelDto Body)
        {
            string token = service.Login(Body);
            return Results.Ok(token);
        }
    }
}