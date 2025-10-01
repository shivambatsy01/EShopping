namespace Catalog.Core.Specs;

public class CatalogSpecParams
{
    private const int maxPageSize = 70;
    private int pageSize = 10;
    
    public int PageIndex { get; set; } = 0;
    public int PageSize
    {
        get => pageSize; 
        set => pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
    public string? BrandId { get; set; }
    public string? TypeId { get; set; }
    public string? Sort { get; set; } //can use enum etc further
    public string? Search { get; set; }
}