using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;


namespace Basket.Application.Handlers.Commands
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {

            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if (coupon is not null)
                {
                    item.Price -= coupon.Amount;
                }
            }
            var shoppingCart = await _basketRepository.UpdateBasket(new Core.Entites.ShoppingCart()
            {
                UserName = request.UserName,
                Items = request.Items,
            });

            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(shoppingCart);

            return shoppingCartResponse;
        }
    }
}
