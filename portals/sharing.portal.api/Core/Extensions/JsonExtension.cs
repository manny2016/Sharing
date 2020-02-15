


namespace Sharing.Portal.Api
{
    using Sharing.Core;
    using Sharing.Core.Models;
    using System;
    using System.Linq;
    public static class JsonExtension
    {
        public static OnlineOrder Convert(this OrderContext context, string tradeid, int code, TradeStates state)
        {
            return new OnlineOrder()
            {
                TradeId = tradeid,
                TradeCode = code.ToString("000"),
                Address = context.Address ?? string.Empty,
                Mobile = context.Tel,
                Name = context.Customer,
                Total = context.Money,
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
                Delivery = context.Delivery,
                State = state,
                CreatedTime = DateTime.Now

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
