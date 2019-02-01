



namespace Sharing.Core.Services
{
    using Microsoft.Extensions.DependencyInjection;
    public static class SharingCoreServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddWeChatUserService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IWeChatUserService), typeof(WeChatUserService), ServiceLifetime.Transient));
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
        public static IServiceCollection AddSharingHostService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(ISharingHostService), typeof(SharingHostService), ServiceLifetime.Singleton));
            return collection;
        }
        public static IServiceCollection AddWeChatMsgHandler(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IWeChatMsgHandler), typeof(WeChatMsgHandler), ServiceLifetime.Transient));
            return collection;
        }
    }
}
