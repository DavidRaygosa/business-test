namespace BusinessTest.Application
{
    using BusinessTest.Business;
    using BusinessTest.Data;
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddServices();
            return services;
        }
    }
}