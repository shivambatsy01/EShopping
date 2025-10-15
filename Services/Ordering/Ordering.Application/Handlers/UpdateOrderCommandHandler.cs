using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
        this._logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        //update all the values in the same object

        var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
        //Other Way is to use mapper.Map : See in testing
        
        //can use reflections here to update all values
        if (orderEntity == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }
        
        UpdateMatchingProperties(request, orderEntity);
        
        await _orderRepository.UpdateAsync(orderEntity);
        _logger.LogInformation($"Order Updated for Id {request.Id}");
        return Unit.Value;
    }
    
    private void UpdateMatchingProperties<TSource, TDestination>(TSource source, TDestination destination)
    {
        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var destinationProperty in destinationProperties)
        {
            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name);

            if (sourceProperty != null)
            {
                var sourcePropertyValue = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, sourcePropertyValue);
            }
        }
    }
}