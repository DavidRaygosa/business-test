namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.ItemStore;
    using BusinessTest.Business.ItemStore.Models;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;

    public class ItemStoreController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/itemstore", GetItemStoreAll).WithName("GetItemStoreAll").WithTags("Item in Store").WithMetadata(new SwaggerOperationAttribute("Get all item in store", "Get all item in store")).Produces<List<ItemStoreResponseModelDto>>(StatusCodes.Status200OK);
            app.MapPost("api/v1/itemstore", PostItemStore).WithName("PostItemStore").WithTags("Item in Store").WithMetadata(new SwaggerOperationAttribute("Add item in store", "Add item in store")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapGet("api/v1/itemstore/{id}", GetItemStore).WithName("GetItemStore").WithTags("Item in Store").WithMetadata(new SwaggerOperationAttribute("Get item in store", "Get item in store")).Produces<ItemStoreResponseModelDto>(StatusCodes.Status200OK);
            app.MapPut("api/v1/itemstore/{id}", PutItemStore).WithName("PutItemStore").WithTags("Item in Store").WithMetadata(new SwaggerOperationAttribute("Update item in store", "Update item in store")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/itemstore/{id}", DeleteItemStore).WithName("DeleteItemStore").WithTags("Item in Store").WithMetadata(new SwaggerOperationAttribute("Delete item in store", "Delete item in store")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.ItemStore_Delete)]
        private IResult DeleteItemStore(IItemStoreAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.ItemStore_Update)]
        private IResult PutItemStore(IItemStoreAppService service, [FromRoute] long? id, [FromBody] ItemStoreRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.ItemStore_Get_All)]
        private IResult GetItemStoreAll(IItemStoreAppService service, [FromQuery] long? ItemId, [FromQuery] long? StoreId)
        {
            List<ItemStoreResponseModelDto> ItemStore = service.GetAll(ItemId, StoreId);
            return Results.Ok(ItemStore);
        }

        [Middlewares.AuthorizePermission(Permissions.ItemStore_Get_One)]
        private IResult GetItemStore(IItemStoreAppService service, [FromRoute] long? id)
        {
            ItemStoreResponseModelDto ItemStore = service.Get(id);
            return Results.Ok(ItemStore);
        }

        [Middlewares.AuthorizePermission(Permissions.ItemStore_Create)]
        private IResult PostItemStore(IItemStoreAppService service, [FromBody] ItemStoreRequestModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }
    }
}