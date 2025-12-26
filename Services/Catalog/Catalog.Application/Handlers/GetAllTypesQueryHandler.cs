using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    private readonly ITypesRepository _typesRepository;
    public GetAllTypesQueryHandler(ITypesRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    
    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("Handling GetAllTypesQuery ................");
            var typesList = await _typesRepository.GetAllProductTypes();
            return MapperExtension.Mapper.Map<IList<TypeResponse>>(typesList);
        }
        catch (Exception ex)
        {
            //log exception
            Console.WriteLine($"Error in GetAllTypesQueryHandler: {ex.Message}");
            throw;
        }
        
    }
}