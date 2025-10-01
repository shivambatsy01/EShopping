using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetAllProductsHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        Pagination<Product> response = await _productRepository.GetAllProducts(request.CatalogSpecParams);
        return MapperExtension.Mapper.Map<Pagination<ProductResponse>>(response);
    }
} 