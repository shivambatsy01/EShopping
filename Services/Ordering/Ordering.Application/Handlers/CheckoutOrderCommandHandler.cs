using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
        this._logger = logger;
    }
    
    
    public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        //Can Use Fluent Validation over OrderRequest
        //Can use error handling
        var orderEntity = _mapper.Map<Order>(request);
        orderEntity.Id = Guid.NewGuid();
        
        var newOrder = await _orderRepository.CreateAsync(orderEntity);
        _logger.LogInformation($"Order {newOrder.Id} is successfully created");
        return newOrder.Id;
        
    }
}