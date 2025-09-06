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
    public class GetAllTypesQueryHandler:IRequestHandler<GetAllTypesQuery, IList<TypeResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITypeRepository _typeRepository;

        public GetAllTypesQueryHandler(
            IMapper mapper,
            ITypeRepository typeRepository
            )
        {
            _mapper = mapper;
            _typeRepository = typeRepository;   
        }

        public async Task<IList<TypeResponseDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typeRepository.GetAllTypes();
            var typeResponseList = _mapper.Map<IList<ProductType>, IList<TypeResponseDto>>(typeList.ToList());
            return typeResponseList;
        }
    }
}
