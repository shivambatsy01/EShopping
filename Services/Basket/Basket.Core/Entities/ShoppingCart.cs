namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string userName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>(); //to avoid null case exception
    
    public ShoppingCart(string userName,  List<ShoppingCartItem> items)
    {
        this.userName = userName;
        this.Items = items;
    }

    public decimal TotalPrice()
    {
        return Items.Sum(i => i.Quantity * i.Price);
    }
}