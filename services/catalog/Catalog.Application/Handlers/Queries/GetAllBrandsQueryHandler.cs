using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandReponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsQueryHandler(
            IMapper mapper,
            IBrandRepository brandRepository
            )
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }
        public async Task<IList<BrandReponseDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetAllBrands();
            var brandResponseList = _mapper.Map<IList<ProductBrand>, IList<BrandReponseDto>>(brandList.ToList());
            return brandResponseList;
        }
    }
}
