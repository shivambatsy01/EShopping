using AutoMapper;
using Basket.Application.ResponsesDTO;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ReverseMap();
        
        CreateMap<ShoppingCart, ShoppingCartResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.userName))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
    }
}