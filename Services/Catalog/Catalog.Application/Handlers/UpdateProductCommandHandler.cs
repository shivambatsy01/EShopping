using Catalog.Application.Commands;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productEntity = await _productRepository.GetProductById(request.ProductId);
            if (productEntity == null)
            {
                throw new Exception("Product not found");
            }
            
            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.Price = request.Price;
            productEntity.Summary = request.Summary;
            productEntity.ImageFile = request.ImageFile;
            productEntity.ProductBrand = request.ProductBrand;
            productEntity.ProductType = request.ProductType;
            
            await _productRepository.UpdateProduct(productEntity);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message); //create custom exception
        }
        
        return true;
    }
}