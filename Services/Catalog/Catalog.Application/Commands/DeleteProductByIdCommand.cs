using MediatR;

namespace Catalog.Application.Commands;

public class DeleteProductByIdCommand : IRequest<bool>
{
    public string ProductId { get; set; }

    public DeleteProductByIdCommand(string productId)
    {
        ProductId = productId;
    }
}