namespace BusinessTest.Application.Middlewares
{
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {
        public readonly string Permission;
        public AuthorizePermissionAttribute() { }
        public AuthorizePermissionAttribute(string permission) => Permission = permission;
        public void OnAuthorization(AuthorizationFilterContext context) { }
    }
}