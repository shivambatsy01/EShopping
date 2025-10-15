using MediatR;

namespace Ordering.Application.Commands;

public class CheckoutOrderCommand : IRequest<Guid> //it will return Id
{
    //apart from OrderId, It will send every other detail
    
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? Email { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }
    public int? PaymentMethod { get; set; }
}