using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class Product : BaseEntity 
{
    [BsonElement("ProductName")]
    public required string Name { get; set; }
    
    public string Summary { get; set; } =  string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string ImageFile { get; set; } = string.Empty;
    
    public ProductBrand ProductBrand { get; set; }
    
    public ProductType ProductType { get; set; }
    
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; } = 0;
    
}