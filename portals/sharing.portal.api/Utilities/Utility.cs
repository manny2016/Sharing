


namespace Sharing.Portal.Api
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Sharing.Core;
    using Sharing.Core.Services;
    public static class Utility
    {
        //private static IServiceProvider provider = null;
        //private static object lockObj = new object();
        //public static IServiceProvider CreateServiceProvider()
        //{
        //    lock (lockObj)
        //    {
        //        if (provider == null)
        //        {
        //            lock (lockObj)
        //            {
        //                if (provider == null)
        //                {
        //                    provider = SharingConfigurations.CreateServiceCollection((collection) =>
        //                    {
        //                        collection.AddWeChatUserService();
        //                    }).BuildServiceProvider();
        //                }
        //            }
        //        }
        //    }           
        //    return provider;
        //}

        public static void Initialize()
        {
            SharingConfigurations.CreateServiceProvider((collection) =>
            {
                collection.AddWeChatUserService();
                collection.AddWeChatPayService();
                collection.AddRandomGenerator();
            });
        }
    }
}
