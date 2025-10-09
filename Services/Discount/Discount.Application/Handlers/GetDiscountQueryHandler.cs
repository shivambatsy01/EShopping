using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscountRepository _repository;
    public GetDiscountQueryHandler(IDiscountRepository repository)
    {
        this._repository = repository;
    }
    
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetCoupon(request.ProductName);

        if (response == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }
        
        return DiscountMapperExtension.Mapper.Map<CouponModel>(response);
    }
}