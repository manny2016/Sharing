


namespace Sharing.Portal.Api
{
    using Sharing.Core.Models;
    using System.Linq;
    public static class JsonExtension
    {
        public static OnlineOrder Convert(this OrderContext context, string tradeid, int code)
        {
            return new OnlineOrder()
            {
                TradeId = tradeid,
                Code = code.ToString("000"),
                Address = context.Address ?? string.Empty,
                Mobile = context.Tel,
                Name = context.Customer,
                Total = context.Money.DecimalValue(),
                Items = context.Details.Select(ctx =>
                {
                    return new OnlineOrderItem()
                    {
                        Count = ctx.Number,
                        Option = ctx.Option,
                        Money = (decimal.Parse(ctx.Price)) * (decimal)ctx.Number,
                        Price = decimal.Parse(ctx.Price),
                        Product = ctx.Name
                    };
                }).ToArray(),
                Fare = context.Fare.DecimalValue(),
                Delivery = context.Delivery
            };
        }
        public static decimal? DecimalValue(this string text)
        {
            if (decimal.TryParse(text, out decimal result))
            {
                return result;
            }
            return null;
        }
    }
}
