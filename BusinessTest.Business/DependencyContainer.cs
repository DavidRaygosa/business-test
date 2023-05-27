namespace BusinessTest.Business
{
    using BusinessTest.Business.Authentication;
    using BusinessTest.Business.Client;
    using BusinessTest.Business.ClientItem;
    using BusinessTest.Business.Item;
    using BusinessTest.Business.ItemStore;
    using BusinessTest.Business.Permission;
    using BusinessTest.Business.Role;
    using BusinessTest.Business.Roles;
    using BusinessTest.Business.Store;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();
            services.AddScoped<IStoreAppService, StoreAppService>();
            services.AddScoped<IItemAppService, ItemAppService>();
            services.AddScoped<IItemStoreAppService, ItemStoreAppService>();
            services.AddScoped<IRoleAppService, RoleAppService>();
            services.AddScoped<IPermissionAppService, PermissionAppService>();
            services.AddScoped<IClientItemAppService, ClientItemAppService>();
            return services;
        }
    }
}