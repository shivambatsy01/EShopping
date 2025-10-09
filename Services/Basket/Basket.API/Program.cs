using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();
services.AddApiVersioning();

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetValue<string>("CacheSettings:RedisProfile:ConnectionString");
});

/*
 * The Repository and DbContext only orchestrate data operations.
 * Repository abd DB context only forwards requests to DB engine by opening the connections
   The actual concurrency, locking, and transaction safety are handled by the database engine.
 */
services.AddScoped<IBasketRepository, BasketRepository>(); //Why repository is scoped ?
services.AddScoped<DiscountGrpcService>(); //Controller injection
services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (o => o.Address = new Uri(configuration["GrpcSettings:DiscountProtoService:Uri"]));

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateShoppingCartCommandHandler).Assembly);
});
services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Basket.API",
        Version = "v1",
        Description = "Example API with Swagger"
    });

    // Optional: Include XML comments for better documentation
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // options.IncludeXmlComments(xmlPath);
});
services.AddHealthChecks()
    .AddRedis(configuration.GetValue<string>("CacheSettings:RedisProfile:ConnectionString"), "Redis Health Check", HealthStatus.Degraded);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();