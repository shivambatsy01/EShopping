using Basket.Application.ResponsesDTO;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands;

public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } //should it be different DTO ?

    public CreateShoppingCartCommand(string userName, List<ShoppingCartItem> shoppingCartItems)
    {
        this.UserName = userName;
        this.ShoppingCartItems = shoppingCartItems;
    }
}