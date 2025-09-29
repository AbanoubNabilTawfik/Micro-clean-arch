using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entites;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketController> _logger;
        

        public BasketController(
            IMediator mediator, 
            IPublishEndpoint publishEndpoint, 
            IMapper mapper,
            ILogger<BasketController> logger

            )
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _logger = logger;
           
        }

        [HttpGet]
        [Route("[action]/{userName}" , Name ="GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
           var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);

        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
         
          var basket = await _mediator.Send(command);
          return Ok(basket);
        }


        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            return Ok(await _mediator.Send(command));

        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //get basket by user name
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await _mediator.Send(query);

            if (basket == null)
            {
                return BadRequest();
            }

            var eventMsg = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMsg.TotalPrice= basket.TotalPrice;
            await _publishEndpoint.Publish(eventMsg);

            _logger.LogInformation($"Basket Published for {basket.UserName}");
            //remove from basket
            var deletedcmd = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
            await _mediator.Send(deletedcmd);
            return Accepted();
        }


    }
}
