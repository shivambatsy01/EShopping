using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        this._orderRepository = orderRepository;
        this._logger = logger;
    }
    
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = await _orderRepository.GetByIdAsync(request.orderId);
        if (orderEntity == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.orderId);
        }
        
        await _orderRepository.DeleteAsync(orderEntity);
        _logger.LogInformation($"Order {orderEntity.Id} has been deleted");
        return Unit.Value;
    }
}