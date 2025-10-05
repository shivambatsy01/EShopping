namespace Basket.Core.Entities;

public class ShoppingCartItem //Item of a shopping cart
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}