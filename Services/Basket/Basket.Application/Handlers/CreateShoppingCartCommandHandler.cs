using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.ResponsesDTO;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler :  IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
    {
        this._basketRepository = basketRepository;
    }
    
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // TODO : Call discount service and apply coupons
        
        var basket = await _basketRepository.UpdateBasket(new ShoppingCart(request.UserName, request.ShoppingCartItems));
        return MapperExtension.Mapper.Map<ShoppingCartResponse>(basket);
    }
}