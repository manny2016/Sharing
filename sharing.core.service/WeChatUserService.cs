

namespace Sharing.Core.Services
{
    using System;
    using Sharing.Core.Models;
    using Sharing.Core;
    using Dapper;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    public class WeChatUserService : IWxUserService
    {

        private readonly IServiceProvider provider = SharingConfigurations.CreateServiceProvider();
        public Membership Register(RegisterWxUserContext context)
        {
            using (var database = SharingConfigurations.GenerateDatabase(true))
            {
                var queryString = @"`spRegisterWeChatUser`";
                var parameters = new DynamicParameters();
                parameters.Add("p_UnionId", context.Info.UnionId, System.Data.DbType.String);
                parameters.Add("p_AppId", context.WxApp.AppId, System.Data.DbType.String);
                parameters.Add("p_OpenId", context.Info.OpenId, System.Data.DbType.String);
                parameters.Add("p_InvitedBy", context.InvitedBy, System.Data.DbType.String);
                parameters.Add("p_Mobile", string.Empty, System.Data.DbType.String);
                parameters.Add("p_RegistrySource", context.AppType, System.Data.DbType.String);
                parameters.Add("p_NickName", context.Info.NickName, System.Data.DbType.String);
                parameters.Add("p_Country", context.Info.Country, System.Data.DbType.String);
                parameters.Add("p_Province", context.Info.Province, System.Data.DbType.String);
                parameters.Add("p_City", context.Info.City, System.Data.DbType.String);
                parameters.Add("p_AvatarUrl", context.Info.AvatarUrl, System.Data.DbType.String);
                parameters.Add("p_CreatedTime", DateTime.Now.ToUnixStampDateTime(), System.Data.DbType.Int64);
                parameters.Add("p_LastActivityTime", DateTime.Now.ToUnixStampDateTime(), System.Data.DbType.Int64);
                parameters.Add("o_Id", null, System.Data.DbType.Int64, System.Data.ParameterDirection.Output);
                parameters.Add("o_mobile", null, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                parameters.Add("o_rewardMoney", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                database.Execute(queryString, parameters, System.Data.CommandType.StoredProcedure);
                return new Membership()
                {
                    AppId = context.WxApp.AppId,
                    Id = parameters.Get<long?>("o_Id"),
                    Mobile = parameters.Get<string>("o_mobile"),
                    RewardMoney = parameters.Get<int?>("o_rewardMoney"),
                    OpenId = context.Info.OpenId
                };
            }
        }

        public IList<ISharedContext> GetSharedContext(IWxUserKey key)
        {
            var cacheKey = string.Format("pyramid_{0}", key.AppId);
            var cache = provider.GetService<IMemoryCache>();
            return cache.GetOrCreate<IList<ISharedContext>>(cacheKey, (entity) =>
            {
                entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                using (var database = SharingConfigurations.GenerateDatabase(false))
                {
                    var queryString = @"SELECT `Id`, `OpenId`,`InvitedBy` FROM `sharing_wxuser` WHERE AppId=@AppId";
                    return database.SqlQuery<SharedContext>(queryString, new { AppId = key.AppId })
                    .ToList<ISharedContext>();
                }
            });
        }



        public long GetWxUserId(IWxUserKey key)
        {
            var context = GetSharedContext(key).FirstOrDefault(o => o.OpenId.Equals(key.OpenId));
            Guard.ArgumentNotNull(context, "SharedContext");
            return context.Id;
        }
    }
}
