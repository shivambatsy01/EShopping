namespace Ordering.Core.Commons;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    
    //Below are the audit properties
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}