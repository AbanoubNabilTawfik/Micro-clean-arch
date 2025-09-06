using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers.Queries
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<GetDiscountQueryHandler> _logger;

        public GetDiscountQueryHandler(
            IDiscountRepository discountRepository,
            ILogger<GetDiscountQueryHandler> logger
            )
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product name ={request.ProductName} not found"));
            }
            var couponModel = new CouponModel
            {
                Id=coupon.Id,
                Amount=coupon.Amount,
                Description=coupon.Description,
                ProductName=coupon.ProductName,
            };
            _logger.LogInformation($"Coupon for the {request.ProductName} is fetched ");
            return couponModel;
        }
    }
}
