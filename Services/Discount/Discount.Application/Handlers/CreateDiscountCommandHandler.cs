using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _repository;
    public CreateDiscountCommandHandler(IDiscountRepository repository)
    {
        this._repository = repository;
    }
    
    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapperExtension.Mapper.Map<Coupon>(request);
        await _repository.CreateCoupon(coupon); //How we are assigning id to it ?, Is this auto increasing in SQL ?
        return DiscountMapperExtension.Mapper.Map<CouponModel>(coupon);
    }
}