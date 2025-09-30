using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{
    //We should also apply fluent validations (reference, Clean architecture course on Udemy)
    
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    public ProductBrand Brand { get; set; } //This one is coming from Entities, Is it okay to keep the one from Entities ? (Because it is derived from BaseEntity, which has Id property of entity)
    public ProductType Type { get; set; } //This one also coming from Entities, Is it okay to keep the one from Entities ? (Because it is derived from BaseEntity, which has Id property of entity)
    
    //Is it okay to have this Core Entities, because anyway we are assuming that we will be providing BrandName and BrandId and same for Type.
    //However the best practice is to create another class for command
}