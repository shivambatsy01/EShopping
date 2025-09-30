using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductType : BaseEntity
{
    [BsonElement("ProductTypeName")]
    public string Name { get; set; } = string.Empty;
    
    //we can further add details, number etc of product type. For now keeping only Name
}