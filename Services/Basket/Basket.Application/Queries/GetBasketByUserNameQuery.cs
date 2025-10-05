using Basket.Application.ResponsesDTO;
using MediatR;

namespace Basket.Application.Queries;

public class GetBasketByUserNameQuery : IRequest<ShoppingCartItemResponse>
{
    public string UserName { get; set; }

    public GetBasketByUserNameQuery(string userName)
    {
        this.UserName = userName;
    }
}