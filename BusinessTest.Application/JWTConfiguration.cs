namespace BusinessTest.Application
{
    using Microsoft.OpenApi.Models;

    public class JWTConfiguration
    {
        public OpenApiSecurityScheme GetScheme()
        {
            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };
            return securityScheme;
        }
        public OpenApiSecurityRequirement GetSecurityRequiement()
        {
            OpenApiSecurityRequirement securityReq = new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            };
            return securityReq;
        }
        public OpenApiContact GetContact()
        {
            OpenApiContact contact = new()
            {
                Name = "David Raygosa",
                Email = "davidro@ibushak.com",
            };
            return contact;
        }
        public OpenApiLicense GetLicense()
        {
            OpenApiLicense license = new()
            {
                Name = "Free License",
                Url = new Uri("http://www.mohamadlawand.com")
            };
            return license;
        }
        public OpenApiInfo GetInfo()
        {
            OpenApiInfo info = new()
            {
                Version = "v1",
                Title = "Datawarehouse API v1",
                Description = "Datawarehouse API v1",
                Contact = GetContact(),
                License = GetLicense()
            };
            return info;
        }
    }
}
