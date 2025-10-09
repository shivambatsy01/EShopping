using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers;

public class DiscountProfile : AutoMapper.Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();  //Model to grpc message

        CreateMap<CreateDiscountCommand, Coupon>().ReverseMap();
    }
}