using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);
        return Mappers.MapperExtension.Mapper.Map<ProductResponse>(product);
    }
}