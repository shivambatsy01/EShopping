using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductBrand, BrandResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
        
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.ImageFile))
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.ImageFile))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ReverseMap();
        
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex))
            .ReverseMap(); //recursively maps Product to product response
        
        CreateMap<ProductType, TypeResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<Product, CreateProductCommand>().ReverseMap();
    }
}