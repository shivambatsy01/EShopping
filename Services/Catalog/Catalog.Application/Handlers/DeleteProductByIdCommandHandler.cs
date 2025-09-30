using Catalog.Application.Commands;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductByIdCommandHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    
    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _productRepository.DeleteProduct(request.ProductId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
        
        return true; //Or use Unit.Value
    }
}