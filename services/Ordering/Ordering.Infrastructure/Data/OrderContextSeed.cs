using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;


namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database :{typeof(OrderContext).Name} seeded!");
            }
        }

        public static IEnumerable<Order> GetOrders()
        {
            return new List<Order> {
                new()
                {
                    UserName="AbanoubNabil",
                    FirstName="Abanoub",
                    LastName="Nabil",
                    EmailAddress="abnabil@eCommerce.net",
                    AddressLine="Cairo",
                    Country="Egypt",
                    TotalPrice=750,
                    State="EG",
                    ZipCode="71111",

                    CardName="Visa",
                    CardNumber="1234567890123456",
                    CreatedBy="Abanoub",
                    Expiration="12/26",
                    Cvv="123",
                    PaymentMethod=1,
                    LastModifiedBy="Abanoub",
                    LastModifiedDate=new DateTime()
                }
            };
        }
    }
}
