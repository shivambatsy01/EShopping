using Catalog.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses;

public class ProductResponse
{
    [BsonElement("ProductName")]
    public required string Name { get; set; }
    
    public string Summary { get; set; } =  string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string ImageFile { get; set; } = string.Empty;
    
    public ProductBrand ProductBrand { get; set; }
    
    public ProductType ProductType { get; set; }
    
    public decimal Price { get; set; } = 0;
}