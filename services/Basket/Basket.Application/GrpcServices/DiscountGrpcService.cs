using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _disocuntGrpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient disocuntGrpcClient)
        {
            _disocuntGrpcClient = disocuntGrpcClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _disocuntGrpcClient.GetDiscountAsync(discountRequest);

        }
    }
}
