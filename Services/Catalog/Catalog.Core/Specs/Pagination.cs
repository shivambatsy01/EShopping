namespace Catalog.Core.Specs;

public class Pagination<T> where T : class
{
    //this will be utilised in Angular application for optimising APIs and queries
    
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> items)
    {
        this.PageIndex = pageIndex;
        this.PageSize = pageSize;
        this.Count = count;
        this.Items = items;
    }

    public Pagination()
    {
        
    }
    
    public int PageIndex  { get; set; }
    public int PageSize { get; set; }
    public int Count  { get; set; }
    
    public int MatchingItemsCount { get; set; }
    public IReadOnlyList<T> Items { get; set; }
}