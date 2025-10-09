using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;

public class CreateDiscountCommand : IRequest<CouponModel>
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }

    public CreateDiscountCommand(string productName, string description, int amount)
    {
        this.ProductName = productName;
        this.Description = description;
        this.Amount = amount;
    }
}