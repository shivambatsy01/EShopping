using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductByIdQuery : IRequest<ProductResponse> //one-response-only
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        this.Id = id;
    }
}