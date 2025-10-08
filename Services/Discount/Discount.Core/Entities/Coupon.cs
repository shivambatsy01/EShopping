namespace Discount.Core.Entities;

public class Coupon
{
    public int Id { get; set; } //Can have Guild etc
    public decimal Amount { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    
}