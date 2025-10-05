namespace Basket.Application.ResponsesDTO;

public class ShoppingCartItemResponse
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}