using System.Reflection;
using Catalog.API;
using Catalog.Application.Handlers;
using Catalog.Core.Repository;
using Catalog.Core.Repository.RepositoryImplementations;
using Catalog.Infra.Data.SeedData.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace CatalogService.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    
    public Startup(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //this same thing we can also do in program.cs file, but we are using .NETCore 5/6 style of startup class
        
        services.AddControllers();
        services.AddApiVersioning();
        services.AddHealthChecks()
            .AddMongoDb(
                dbFactory: sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var connectionString = configuration["DatabaseSettings:MongoCatalog:ConnectionStrings"];

                    var client = new MongoClient(connectionString);
                    return client.GetDatabase("ProductCatalogDb"); //picked from AppSettings.Json
                },
                name: "CatalogService MongoDB Health Check",
                failureStatus: HealthStatus.Degraded
            );
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", 
                new OpenApiInfo
                {
                    Title = "ProductCatalog.API",
                    Version = "v1"
                });
        });
        
        //Dependency Injections
        services.AddAutoMapper(
            config => { }, 
            AppDomain.CurrentDomain.GetAssemblies()
        );
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly); //Why only one command handler, what does it mean ?
            //Scan this assembly for any classes that handle commands, queries, or notifications, and automatically register them in the dependency injection container.
        });
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>(); //all three interfaces are implemented by ProductRepository
        services.AddScoped<ITypesRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, ProductRepository>();



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
        }
        
        // app.Build();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); //mapped to controllers
            //health-check URL is defined
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });
        
    }
}