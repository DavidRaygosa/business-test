namespace BusinessTest.Data
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Data.Repositories;
    using BusinessTest.Data.Repositories.Authentication;
    using BusinessTest.Data.Repositories.Client;
    using BusinessTest.Data.Repositories.ClientItem;
    using BusinessTest.Data.Repositories.Item;
    using BusinessTest.Data.Repositories.ItemStore;
    using BusinessTest.Data.Repositories.Permission;
    using BusinessTest.Data.Repositories.Role;
    using BusinessTest.Data.Repositories.Store;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessTestDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(configuration["Env"])));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemStoreRepository, ItemStoreRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IClientItemRepository, ClientItemRepository>();
            return services;
        }
    }
}