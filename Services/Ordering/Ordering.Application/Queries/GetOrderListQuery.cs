using MediatR;
using Ordering.Application.ResponseDTOs;

namespace Ordering.Application.Queries;

public class GetOrderListQuery : IRequest<List<OrderResponse>>
{
    public string Username { get; set; }

    public GetOrderListQuery(string username)
    {
        this.Username = username;
    }
    
}