using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers;

//We have to inherit or implement anything else because everything will be implemented from APIBaseController
//these controllers will inject mediators and everything mediator will take care

public class CalatogController : APIBaseController
{
    private readonly IMediator _mediator;
    public CalatogController(IMediator mediator)
    {
        this._mediator = mediator;
    }
    
    
    
    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> GetProductByProductId(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductByProductName")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams specParams) //later implement pagination etc
    {
        var query = new GetAllProductsQuery(specParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{brandName}", Name = "GetProductsByBrandName")] //ASP.NET Core will replace [action] with the method name at runtime.
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brandName)
    {
        var query = new GetProductsByBrandQuery(brandName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(BrandResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands() //later implement pagination etc
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(TypeResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes() //later implement pagination etc
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand commandRequest)
    {
        //In this flow, Please check if we are creating id anywhere or MongoDB take care of it by itself ?
        var result = await _mediator.Send(commandRequest);
        return Ok(result);
    }
    
    [HttpPut] //Put as we are sending almost entire object
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand commandRequest)
    {
        var result = await _mediator.Send(commandRequest);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("[action]/{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        DeleteProductByIdCommand commandRequest = new DeleteProductByIdCommand(id);
        var result = await _mediator.Send(commandRequest);
        return Ok(result);
    }
}