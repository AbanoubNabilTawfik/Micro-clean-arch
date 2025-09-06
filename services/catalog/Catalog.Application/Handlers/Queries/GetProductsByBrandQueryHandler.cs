using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductReponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByBrandQueryHandler(
            IProductRepository ProductRepository,
            IMapper mapper
            )
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<IList<ProductReponseDto>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsByBrand(request.BrandName);

            var productResponseList = _mapper.Map<IList<ProductReponseDto>>(products);

            return productResponseList;
        }
    }
}
