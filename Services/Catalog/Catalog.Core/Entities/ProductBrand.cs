using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductBrand : BaseEntity
{
    [BsonElement("BrandName")]
    public string Name { get; set; } =  string.Empty; //default empty string
    
    //we can further add details etc of this product brand
}