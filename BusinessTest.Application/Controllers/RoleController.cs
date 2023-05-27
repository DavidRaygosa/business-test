namespace BusinessTest.Application.Controllers
{
    using BusinessTest.Application.Models;
    using BusinessTest.Business.Role.Models;
    using BusinessTest.Business.Roles;
    using Carter;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    public class RoleController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/role", GetRoleAll).WithName("GetRoleAll").WithTags("Role").WithMetadata(new SwaggerOperationAttribute("Get all roles", "Get all roles")).Produces<List<RoleResponseModelDto>>(StatusCodes.Status200OK);
            app.MapPost("api/v1/role", PostRole).WithName("PostRole").WithTags("Role").WithMetadata(new SwaggerOperationAttribute("Create role", "Create role")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapGet("api/v1/role/{id}", GetRole).WithName("GetRole").WithTags("Role").WithMetadata(new SwaggerOperationAttribute("Get role", "Get role")).Produces<RoleResponseModelDto>(StatusCodes.Status200OK);
            app.MapPut("api/v1/role/{id}", PutRole).WithName("PutRole").WithTags("Role").WithMetadata(new SwaggerOperationAttribute("Update role", "Update role")).Produces<Boolean>(StatusCodes.Status200OK);
            app.MapDelete("api/v1/role/{id}", DeleteRole).WithName("DeleteRole").WithTags("Role").WithMetadata(new SwaggerOperationAttribute("Delete role", "Delete role")).Produces<Boolean>(StatusCodes.Status200OK);
        }

        [Middlewares.AuthorizePermission(Permissions.Role_Delete)]
        private IResult DeleteRole(IRoleAppService service, [FromRoute] long? id)
        {
            service.Delete(id);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Role_Update)]
        private IResult PutRole(IRoleAppService service, [FromRoute] long? id, [FromBody] RoleUpdateRequestModelDto Body)
        {
            service.Update(id, Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Role_Get_One)]
        private IResult GetRole(IRoleAppService service, [FromRoute] long? id)
        {
            RoleResponseModelDto role = service.Get(id);
            return Results.Ok(role);
        }

        [Middlewares.AuthorizePermission(Permissions.Role_Create)]
        private IResult PostRole(IRoleAppService service, [FromBody] RoleRequestModelDto Body)
        {
            service.Create(Body);
            return Results.Ok(true);
        }

        [Middlewares.AuthorizePermission(Permissions.Role_Get_All)]
        private IResult GetRoleAll(IRoleAppService service)
        {
            List<RoleResponseModelDto> roles = service.GetAll();
            return Results.Ok(roles);
        }
    }
}