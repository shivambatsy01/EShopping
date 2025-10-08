using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;



services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateDiscountCommandHandler).Assembly);
});
services.AddScoped<IDiscountRepository, DiscountRepository>();
services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddGrpc();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();

// Configure the HTTP request pipeline. 
app.MapGrpcService<DiscountService>();
app.MapGet("/", async context  =>
{
    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client." +
                                      " To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
});
app.MigrateDatabase<Program>();

app.Run();