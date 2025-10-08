using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _repository;
    public UpdateDiscountCommandHandler(IDiscountRepository repository)
    {
        this._repository = repository;
    }
    
    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapperExtension.Mapper.Map<Coupon>(request);
        await _repository.UpdateCoupon(coupon);
        return DiscountMapperExtension.Mapper.Map<CouponModel>(coupon);
    }
}