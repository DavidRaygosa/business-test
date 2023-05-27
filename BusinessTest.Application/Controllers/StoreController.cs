namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.Store;
    using BusinessTest.Business.Store.Models;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;

    public class StoreController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/store", GetStores).WithName("StoreGetAll").WithTags("Store").WithMetadata(new SwaggerOperationAttribute("Get all store", "Get all store")).Produces<List<StoreResponseModelDto>>(StatusCodes.Status200OK);
            app.MapGet("api/v1/store/{id}", GetStore).WithName("StoreGet").WithTags("Store").WithMetadata(new SwaggerOperationAttribute("Get store", "Get store")).Produces<StoreResponseModelDto>(StatusCodes.Status200OK);
            app.MapPost("api/v1/store", PostStore).WithName("StorePost").WithTags("Store").WithMetadata(new SwaggerOperationAttribute("Create store", "Create store")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapPut("api/v1/store/{id}", PutStore).WithName("StorePut").WithTags("Store").WithMetadata(new SwaggerOperationAttribute("Update store", "Update store")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/store/{id}", DeleteStore).WithName("StoreDelete").WithTags("Store").WithMetadata(new SwaggerOperationAttribute("Delete store", "Delete store")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Store_Delete)]
        private IResult DeleteStore(IStoreAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Store_Get_One)]
        private IResult GetStore(IStoreAppService service, [FromRoute] long? id)
        {
            StoreResponseModelDto Store = service.Get(id);
            return Results.Ok(Store);
        }

        [Middlewares.AuthorizePermission(Permissions.Store_Get_All)]
        private IResult GetStores(IStoreAppService service)
        {
            List<StoreResponseModelDto> Stores = service.GetAll();
            return Results.Ok(Stores);
        }

        [Middlewares.AuthorizePermission(Permissions.Store_Update)]
        private IResult PutStore(IStoreAppService service, [FromRoute] long? id, [FromBody] StoreRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Store_Create)]
        private IResult PostStore(IStoreAppService service, [FromBody] StoreRequestModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }
    }
}