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
        var typesList = await _typesRepository.GetAllProductTypes();
        return MapperExtension.Mapper.Map<IList<TypeResponse>>(typesList);
    }
}