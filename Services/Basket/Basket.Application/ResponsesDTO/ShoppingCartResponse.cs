namespace Basket.Application.ResponsesDTO;

public class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; set; } //in Entity Objects, we have null checks, hence this can be null

    public ShoppingCartResponse()
    {
        
    }

    public ShoppingCartResponse(string userName)
    {
        this.UserName = userName;
    }

    public decimal GetTotalPrice()
    {
        return Items.Sum(i => i.Price * i.Quantity);
    }
}