using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;
    public DiscountRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    
    public async Task<Coupon> GetCoupon(string productName)
    {
        //Can we use DBContext for below command statements ?
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DatabaseSettings:postgres:ConnectionString"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

        if (coupon == null)
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Available" };
        
        return coupon;
    }

    public async Task<bool> CreateCoupon(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DatabaseSettings:postgres:ConnectionString"));

        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new {ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

        if (affected == 0) return false;

        return true;
    }

    public async Task<bool> UpdateCoupon(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DatabaseSettings:postgres:ConnectionString"));

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        return affected > 0;
    }

    public async Task<bool> DeleteCoupon(string productName)
    {
        await using var connection = new NpgsqlConnection(
            _configuration.GetConnectionString("DatabaseSettings:postgres:ConnectionString"));

        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { productName = productName });

        return affected > 0;
    }
}