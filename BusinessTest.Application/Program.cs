using BusinessTest.Application;
using BusinessTest.Application.Extensions;
using BusinessTest.Application.Middlewares;
using Carter;
using Carter.Response;

var builder = WebApplication.CreateBuilder(args);
JWTConfiguration jtwConfiguration = new();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencies(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = $"Business API v1 ({builder.Configuration["Env"]})", Version = "v1", Description = "apiBusiness" });
    c.DocInclusionPredicate((docName, description) => true);
    c.AddSecurityDefinition("Bearer", jtwConfiguration.GetScheme());
    c.AddSecurityRequirement(jtwConfiguration.GetSecurityRequiement());
});

builder.Services.AddCarter(configurator: c =>
{
    c.WithResponseNegotiator<NewtonsoftJsonResponseNegotiator>();
});

var app = builder.Build();

app.MapCarter();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Business API v1");
    options.DisplayRequestDuration();
    options.EnableFilter();
    options.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseDirectoryBrowser();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseSecurityMiddleware();

app.Run();