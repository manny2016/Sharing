using Microsoft.Extensions.DependencyInjection;
using Sharing.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public static class SharingCoreServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddWeChatApiService(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IWeChatApi), typeof(WeChatApiService), ServiceLifetime.Transient));
            return services;
        }
    
    }
}
