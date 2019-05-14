using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sharing.WeChat.Models;
using System.Threading;
using Sharing.Core.Models;
using Sharing.Core.Services;
using Sharing.Core.CMQ;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = TencentCMQClientFactory.Create<OnlineOrder>("lemon");
            client.Initialize();

            //var orders = new OnlineOrder[] {
            //     new OnlineOrder(){
            //          Name = "乔**",
            //           Address = "宜兴市西南区128号",
            //            Items = new OnlineOrderItem[]{
            //                 new OnlineOrderItem(){
            //                      Product = "波霸奶茶",
            //                       Option = "大杯,常温,少糖",
            //                        Price = 1000,
            //                        Money= 1000,
            //                         Count = 1
            //                 },
            //                 new OnlineOrderItem(){
            //                      Product = "鸡米花",
            //                       Option = "无",
            //                        Price = 1000,
            //                        Money= 1000,
            //                        Count = 1
            //                 }
            //            }
            //     },
            //       new OnlineOrder(){
            //          Name = "乔**",
            //           Address = "宜兴市西南区128号",
            //            Items = new OnlineOrderItem[]{
            //                 new OnlineOrderItem(){
            //                      Product = "波霸奶茶",
            //                       Option = "大杯,常温,少糖",
            //                        Price = 1000,
            //                        Money= 1000,
            //                         Count = 1
            //                 },
            //                 new OnlineOrderItem(){
            //                      Product = "鸡米花",
            //                       Option = "无",
            //                        Price = 1000,
            //                        Money= 1000,
            //                        Count = 1
            //                 }
            //            }
            //     }
            //};
            //client.Push(orders);

            client.Monitor((entity) =>
            {
                Console.WriteLine($"Online Order:{entity.Name}");
                client.DeleteMessage(entity);
            });
        }

    }
}
