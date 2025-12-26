using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<GetDiscountQueryHandler> _logger;
    public GetDiscountQueryHandler(IDiscountRepository repository, ILogger<GetDiscountQueryHandler> logger)
    {
        this._repository = repository;
        this._logger = logger;
    }
    
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"GetDiscountQueryHandler handling request for ProductName: {request.ProductName}");
        
        var response = await _repository.GetCoupon(request.ProductName);

        if (response == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }
        
        return DiscountMapperExtension.Mapper.Map<CouponModel>(response);
    }
}