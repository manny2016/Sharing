



namespace Sharing.Core.Services
{
    using Microsoft.Extensions.DependencyInjection;
    public static class SharingCoreServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddWeChatUserService(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IWxUserService), typeof(WeChatUserService), ServiceLifetime.Transient));            
            return services;
        }
        public static IServiceCollection AddMcardService(this IServiceCollection services)
        {            
            services.Add(new ServiceDescriptor(typeof(IMCardService), typeof(MCardService), ServiceLifetime.Transient));
            return services;
        }
    }
}
