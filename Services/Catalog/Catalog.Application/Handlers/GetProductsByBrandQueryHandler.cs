using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetProductsByBrandQueryHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productsList = await _productRepository.GetProductsByBrand(request.BrandName);
        return MapperExtension.Mapper.Map<List<ProductResponse>>(productsList);
    }
}