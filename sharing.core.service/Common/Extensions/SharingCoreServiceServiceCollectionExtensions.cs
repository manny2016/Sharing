



namespace Sharing.Core.Services
{
    using Microsoft.Extensions.DependencyInjection;
    public static class SharingCoreServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddWeChatUserService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IWxUserService), typeof(WeChatUserService), ServiceLifetime.Transient));
            return collection;
        }
        public static IServiceCollection AddMcardService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IMCardService), typeof(MCardService), ServiceLifetime.Transient));
            return collection;
        }
        public static IServiceCollection AddWeChatPayService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IWeChatPayService), typeof(WeChatPayService), ServiceLifetime.Transient));
            return collection;
        }
    }
}
