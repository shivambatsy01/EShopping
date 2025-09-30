 using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllBrandsHandler :  IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository; //declared in Core and defined in Infrastructure
    
    public GetAllBrandsHandler(IBrandRepository brandRepository) //dependency injection
    {
        this._brandRepository = brandRepository;
    }
    
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllBrands();
        //var brandResponseList = _mapper.Map<IList<BrandResponse>>(brandList); //no List-to-List mapping
        //var brandResponseList = _mapper.Map<IList<BrandResponse>>(brandList); //use ForMember in mapper for List-to-List mapping
        var brandResponseList = MapperExtension.Mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brandList.ToList());
        return brandResponseList;
    }
}