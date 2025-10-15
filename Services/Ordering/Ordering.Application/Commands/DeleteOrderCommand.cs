using MediatR;

namespace Ordering.Application.Commands;

public class DeleteOrderCommand : IRequest<Unit>
{
    public Guid orderId;
}