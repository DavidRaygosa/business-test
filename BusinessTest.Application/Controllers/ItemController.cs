namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.Item;
    using BusinessTest.Business.Item.Models;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;

    public class ItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/item", GetItemAll).WithName("GetItemAll").WithTags("Item").WithMetadata(new SwaggerOperationAttribute("Get all item", "Get all item")).Produces<List<ItemResponseModelDto>>(StatusCodes.Status200OK);
            app.MapPost("api/v1/item", PostItem).WithName("PostItem").WithTags("Item").WithMetadata(new SwaggerOperationAttribute("Create item", "Create item")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapGet("api/v1/item/{id}", GetItem).WithName("GetItem").WithTags("Item").WithMetadata(new SwaggerOperationAttribute("Get item", "Get item")).Produces<ItemResponseModelDto>(StatusCodes.Status200OK);
            app.MapPut("api/v1/item/{id}", PutItem).WithName("PutItem").WithTags("Item").WithMetadata(new SwaggerOperationAttribute("Update item", "Update item")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/item/{id}", DeleteItem).WithName("DeleteItem").WithTags("Item").WithMetadata(new SwaggerOperationAttribute("Delete item", "Delete item")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Item_Delete)]
        private IResult DeleteItem(IItemAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Item_Get_One)]
        private IResult GetItem(IItemAppService service, [FromRoute] long? id)
        {
            ItemResponseModelDto Item = service.Get(id);
            return Results.Ok(Item);
        }

        [Middlewares.AuthorizePermission(Permissions.Item_Get_All)]
        private IResult GetItemAll(IItemAppService service)
        {
            List<ItemResponseModelDto> Items = service.GetAll();
            return Results.Ok(Items);
        }

        [Middlewares.AuthorizePermission(Permissions.Item_Update)]
        private IResult PutItem(IItemAppService service, [FromRoute] long? id, [FromBody] ItemRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Item_Create)]
        private IResult PostItem(IItemAppService service, [FromBody] ItemRequestModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }
    }
}