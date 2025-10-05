using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache; //redis : <userName, Serialised string <ShoppingCart>> key-Value pair
    public BasketRepository(IDistributedCache distributedCache)
    {
        _redisCache = distributedCache;
    }
    
    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }
        
        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart newCart)
    {
        await _redisCache.SetStringAsync(newCart.userName, JsonConvert.SerializeObject(newCart));
        return await GetBasket(newCart.userName); //utilise above method
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}