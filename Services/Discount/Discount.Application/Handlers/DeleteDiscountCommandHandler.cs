using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _repository;
    public DeleteDiscountCommandHandler(IDiscountRepository repository)
    {
        this._repository = repository;
    }
    
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _repository.DeleteCoupon(request.ProductName);
        return isDeleted;
    }
}