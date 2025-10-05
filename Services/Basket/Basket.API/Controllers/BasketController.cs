using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.ResponsesDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketController : ApiBaseController
{
    private readonly IMediator _mediator;
    public BasketController(IMediator mediator)
    {
        this._mediator = mediator;
    }
    
    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket([FromQuery] string userName)
    {
        var request = new GetBasketByUserNameQuery(userName);
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket([FromQuery] string userName)
    {
        var request = new DeleteBasketByUserNameCommand(userName);
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    
}