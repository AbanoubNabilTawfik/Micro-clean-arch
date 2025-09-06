using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator   = mediator;
        }
        [HttpGet]
        [Route("[action]/{id}",Name ="GetProductById")]
        [ProducesResponseType(typeof(ProductReponseDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductReponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductsByProductName")]
        [ProducesResponseType(typeof(IList<ProductReponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> GetProductsByProductName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductReponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> GetAllProducts([FromQuery] CatalogSpecParams specs)
        {
            var query = new GetAllProductsQuery(specs);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandReponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BrandReponseDto>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TypeResponseDto>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductReponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send<ProductReponseDto>(productCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send<bool>(productCommand);
            return Ok(result);
        }


        [HttpPut]
        [Route("{id}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send<bool>(command);
            return Ok(result);
        }


    }
}
