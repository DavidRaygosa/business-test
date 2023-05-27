namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.Permission;
    using BusinessTest.Business.Permission.Models;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Swashbuckle.AspNetCore.Annotations;

    public class PermissionController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/v1/permission", PostPermission).WithName("PostPermission").WithTags("Permission").WithMetadata(new SwaggerOperationAttribute("Create permission", "Create permission")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapGet("api/v1/permission", GetPermissionAll).WithName("GetPermissionAll").WithTags("Permission").WithMetadata(new SwaggerOperationAttribute("Get all permissions", "Get all permissions")).Produces<List<PermissionResponseModelDto>>(StatusCodes.Status200OK);
            app.MapGet("api/v1/permission/{id}", GetPermission).WithName("GetPermission").WithTags("Permission").WithMetadata(new SwaggerOperationAttribute("Get permission", "Get permission")).Produces<PermissionResponseModelDto>(StatusCodes.Status200OK);
            app.MapPut("api/v1/permission/{id}", PutPermission).WithName("PutPermission").WithTags("Permission").WithMetadata(new SwaggerOperationAttribute("Update permission", "Update permission")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/permission/{id}", DeletePermission).WithName("DeletePermission").WithTags("Permission").WithMetadata(new SwaggerOperationAttribute("Delete permission", "Delete permission")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Permission_Delete)]
        private IResult DeletePermission(IPermissionAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Permission_Update)]
        private IResult PutPermission(IPermissionAppService service, [FromRoute] long? id, [FromBody] PermissionRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Permission_Get_One)]
        private IResult GetPermission(IPermissionAppService service, [FromRoute] long? id)
        {
            PermissionResponseModelDto permission = service.Get(id);
            return Results.Ok(permission);
        }

        [Middlewares.AuthorizePermission(Permissions.Permission_Get_All)]
        private IResult GetPermissionAll(IPermissionAppService service)
        {
            List<PermissionResponseModelDto> permissions = service.GetAll();
            return Results.Ok(permissions);
        }

        [Middlewares.AuthorizePermission(Permissions.Permission_Create)]
        private IResult PostPermission(IPermissionAppService service, [FromBody] PermissionRequestModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }
    }
}