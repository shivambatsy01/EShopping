using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Discount DB Migration started...");
                ApplyMigrations(configuration);
                logger.LogInformation("Discount DB Migration completed");
            }
            catch (Exception e)
            {
                logger.LogError($"Discount DB migration failed with exception : {e.ToString()}");
            }
        }
        
        return host;
    }

    private static void ApplyMigrations(IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseSettings:postgres:ConnectionString"];
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };
        cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
        cmd.ExecuteNonQuery();
        
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY,
                                                                ProductName VARCHAR(500) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
        cmd.ExecuteNonQuery();
        
        
        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500)";
        cmd.ExecuteNonQuery();
        
        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex vCore Pro 100 A Tennis Racquet (270gm Strung)', 'Racquet Discount', 300)";
        cmd.ExecuteNonQuery();
    }
}