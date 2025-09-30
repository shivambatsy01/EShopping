using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductsQuery : IRequest<List<ProductResponse>> //Dont return DTO's
{
    /*
     * we can further segregate this into features like :
     * Features :
     *      Products : Get,Create,Update etc....
     *      Brands : .......
     *      Types : ......
     *
     *
     * There are different ways to structure the code
     * Say if We are designing handler s for generic entities, then there is no point to separate by entity
     */
    
}