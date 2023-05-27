namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.Client;
    using BusinessTest.Business.Client.Models;
    using Carter;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    public class ClientController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/client", GetClients).WithName("ClientGet").WithTags("Client").WithMetadata(new SwaggerOperationAttribute("Get clients", "Get Clients")).Produces<List<ClientModelDto>>(StatusCodes.Status200OK);
            app.MapGet("api/v1/client/{id}", GetOneClient).WithName("ClientGetOne").WithTags("Client").WithMetadata(new SwaggerOperationAttribute("Get one client", "Get one Client")).Produces<ClientModelDto>(StatusCodes.Status200OK);
            app.MapPost("api/v1/client", PostClient).WithName("ClientPost").WithTags("Client").WithMetadata(new SwaggerOperationAttribute("Create client", "Create Client")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapPut("api/v1/client/{id}", PutClient).WithName("ClientPut").WithTags("Client").WithMetadata(new SwaggerOperationAttribute("Update client", "Update Client")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/client/{id}", DeleteClient).WithName("ClientDelete").WithTags("Client").WithMetadata(new SwaggerOperationAttribute("Delete client", "Delete Client")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Client_Delete)]
        private IResult DeleteClient(IClientAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Client_Update)]
        private IResult PutClient(IClientAppService service, [FromRoute] long? id, [FromBody] UpdateClientModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Client_Get_All)]
        private IResult GetClients(IClientAppService service)
        {
            List<ClientModelDto> Clients = service.GetAll();
            return Results.Ok(Clients);
        }

        [Middlewares.AuthorizePermission(Permissions.Client_Get_One)]
        private IResult GetOneClient(IClientAppService service, [FromRoute] long? id)
        {
            ClientModelDto Client = service.GetOne(id);
            return Results.Ok(Client);
        }

        [AllowAnonymous]
        private IResult PostClient(IClientAppService service, [FromBody] CreateClientModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }
    }
}