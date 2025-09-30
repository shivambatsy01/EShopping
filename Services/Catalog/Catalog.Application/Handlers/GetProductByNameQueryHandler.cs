using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<ProductResponse> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductsByName(request.Name);
        return Mappers.MapperExtension.Mapper.Map<ProductResponse>(product);
    }
}