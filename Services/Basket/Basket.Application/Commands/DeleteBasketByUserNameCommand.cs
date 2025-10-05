using Basket.Application.ResponsesDTO;
using MediatR;

namespace Basket.Application.Commands;

public class DeleteBasketByUserNameCommand : IRequest<Unit>
{
    public string UserName { get; set; }

    public DeleteBasketByUserNameCommand(string userName)
    {
        this.UserName = userName;
    }
}