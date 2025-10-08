using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase //This is a service rather than controller : This is the difference
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountQuery(request.ProductName);
        var response = await _mediator.Send(query);
        _logger.LogInformation($"GetDiscount API response: {response} for product name: {request.ProductName}");
        return response;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountCommand(request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);
        var response = await _mediator.Send(command);
        _logger.LogInformation($"Created new discount API response: {response}");
        return response;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountCommand
        {
            Id = request.Coupon.Id,
            Amount = request.Coupon.Amount,
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description
        };
        var response = await _mediator.Send(command);
        _logger.LogInformation($"Updated discount response: {response} for discount Id: {request.Coupon.Id}");
        return response;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleteCommand = new DeleteDiscountCommand
        {
            ProductName = request.ProductName
        };
        var isDeleted = await _mediator.Send(deleteCommand);
        _logger.LogInformation($"Discount with productName: {request.ProductName} was deleted");
        return new DeleteDiscountResponse
        {
            Success = isDeleted
        };
    }
}