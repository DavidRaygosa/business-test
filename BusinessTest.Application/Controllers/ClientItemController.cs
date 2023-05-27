namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.ClientItem;
    using BusinessTest.Business.ClientItem.Models;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;

    public class ClientItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/clientitem", GetClientItemAll).WithName("GetClientItemAll").WithTags("Client with item").WithMetadata(new SwaggerOperationAttribute("Get all item from clients", "Get all item from clients")).Produces<List<ClientItemResponseModelDto>>(StatusCodes.Status200OK);
            app.MapPost("api/v1/clientitem", PostClientItem).WithName("PostClientItem").WithTags("Client with item").WithMetadata(new SwaggerOperationAttribute("Add item to client", "Add item to client")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapGet("api/v1/clientitem/{id}", GetClientItem).WithName("GetClientItem").WithTags("Client with item").WithMetadata(new SwaggerOperationAttribute("Get item from client", "Get item from client")).Produces<ClientItemResponseModelDto>(StatusCodes.Status200OK);
            app.MapPut("api/v1/clientitem/{id}", PutClientItem).WithName("PutClientItem").WithTags("Client with item").WithMetadata(new SwaggerOperationAttribute("Update item from client", "Update item from client")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/clientitem/{id}", DeleteClientItem).WithName("DeleteClientItem").WithTags("Client with item").WithMetadata(new SwaggerOperationAttribute("Delete item from client", "Delete item from client")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.ClientItem_Delete)]
        private IResult DeleteClientItem(IClientItemAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.ClientItem_Update)]
        private IResult PutClientItem(IClientItemAppService service, [FromRoute] long? id, [FromBody] ClientItemRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.ClientItem_Get_One)]
        private IResult GetClientItem(IClientItemAppService service, [FromRoute] long? id)
        {
            ClientItemResponseModelDto itemClient = service.Get(id);
            return Results.Ok(itemClient);
        }

        [Middlewares.AuthorizePermission(Permissions.ClientItem_Get_All)]
        private IResult GetClientItemAll(IClientItemAppService service, [FromQuery] long? ItemId, [FromQuery] long? ClientId)
        {
            List<ClientItemResponseModelDto> itemClients = service.GetAll(ItemId, ClientId);
            return Results.Ok(itemClients);
        }

        [Middlewares.AuthorizePermission(Permissions.ClientItem_Create)]
        private IResult PostClientItem(IClientItemAppService service, [FromHeader] string? Authorization, [FromBody] ClientItemRequestModelDto Body)
        {
            service.Create(Authorization, Body);
            return Results.Ok(true);
        }
    }
}
