using AutoMapper;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.ResponsesDTO;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartItemResponse>
{
    private readonly IBasketRepository _basketRepository;
    public GetBasketByUserNameQueryHandler(IMapper mapper, IBasketRepository basketRepository)
    {
        this._basketRepository = basketRepository;
    }
    
    public async Task<ShoppingCartItemResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var basketObject = await _basketRepository.GetBasket(request.UserName);
        return MapperExtension.Mapper.Map<ShoppingCartItemResponse>(basketObject); //No need to inject IMapper
    }
}