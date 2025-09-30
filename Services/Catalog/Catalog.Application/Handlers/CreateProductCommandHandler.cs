using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = MapperExtension.Mapper.Map<Product>(request);
        if (productToAdd == null)
        {
            throw new ApplicationException("Product entity can not be created : Issue in Mapper");
        }
        
        //Where we are creating Id of this product ?
        
        var newProductEntity = await _productRepository.AddProduct(productToAdd);

        if (newProductEntity == null)
        {
            throw new ApplicationException("Product could not be added"); //Use custom exceptions
        }
        
        return MapperExtension.Mapper.Map<ProductResponse>(newProductEntity);
    }
}