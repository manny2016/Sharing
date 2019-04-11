

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
    using System.Data;
    public class WeChatUserService : IWeChatUserService
    {
        private readonly IMemoryCache cache;
        public WeChatUserService(IMemoryCache cache)
        {
            this.cache = cache;
        }
        public Membership Register(RegisterWxUserContext context)
        {
            using (var database = SharingConfigurations.GenerateDatabase(true))
            {

                var queryString = @"`spRegisterWeChatUser`";
                var parameters = new DynamicParameters();
                parameters.Add("p_UnionId", context.Info.UnionId, System.Data.DbType.String);                
                parameters.Add("p_AppId", context.WxApp.AppId, System.Data.DbType.String);
                parameters.Add("p_OpenId", context.Info.OpenId, System.Data.DbType.String);
                parameters.Add("p_RegistrySource", context.AppType.ToString(), System.Data.DbType.String);
                parameters.Add("p_NickName", context.Info.NickName, System.Data.DbType.String);
                parameters.Add("p_Country", context.Info.Country, System.Data.DbType.String);
                parameters.Add("p_Province", context.Info.Province, System.Data.DbType.String);
                parameters.Add("p_City", context.Info.City, System.Data.DbType.String);
                parameters.Add("p_AvatarUrl", context.Info.AvatarUrl, System.Data.DbType.String);
                parameters.Add("p_CreatedTime", DateTime.UtcNow.ToUnixStampDateTime(), System.Data.DbType.Int64);
                parameters.Add("p_LastActivityTime", DateTime.UtcNow.ToUnixStampDateTime(), System.Data.DbType.Int64);
                parameters.Add("o_Id", null, System.Data.DbType.Int64, System.Data.ParameterDirection.Output);
                parameters.Add("o_mobile", null, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                parameters.Add("o_rewardMoney", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                parameters.Add("o_mchid", null, System.Data.DbType.Int64, System.Data.ParameterDirection.Output);
                database.Execute(queryString, parameters, System.Data.CommandType.StoredProcedure, true);
                return new Membership()
                {
                    AppId = context.WxApp.AppId,
                    Id = parameters.Get<long?>("o_Id"),
                    Mobile = parameters.Get<string>("o_mobile"),
                    RewardMoney = parameters.Get<int?>("o_rewardMoney"),
                    MchId = parameters.Get<long?>("o_mchid"),
                    OpenId = context.Info.OpenId
                };

            }
        }

        public IList<ISharedContext> GetSharedContext(IMchId mch)
        {
            var cacheKey = string.Format("pyramid_{0}", mch.MchId);
            return this.cache.GetOrCreate<IList<ISharedContext>>(cacheKey, (entity) =>
            {
                entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                using (var database = SharingConfigurations.GenerateDatabase(false))
                {
                    var queryString = @"SELECT `WxUserId` AS Id,`MchId`,`InvitedBy` FROM `sharing_sharingcontext` WHERE `MchId`=@MchId";
                    return database.SqlQuery<SharedContext>(queryString, new { MchId = mch.MchId })
                    .ToList<ISharedContext>();
                }
            });
        }


        public long GetWxUserId(IWxUserKey key)
        {
            var context = GetSharedContext(key).FirstOrDefault(o => o.Id.Equals(key.Id));
            Guard.ArgumentNotNull(context, "SharedContext");
            return context.Id;
        }

        public void RegisterCardCoupon(RegisterCardCoupon registerCard)
        {

            using (var database = SharingConfigurations.GenerateDatabase(true))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_AppId", registerCard.AppId);
                parameters.Add("p_OpenId", registerCard.OpenId);
                parameters.Add("p_UnionId", registerCard.UnionId);
                parameters.Add("p_CardId", registerCard.CardId);
                parameters.Add("p_UserCode", registerCard.UserCode);
                parameters.Add("p_IsGiveByFriend", registerCard.IsGiveByFriend);
                parameters.Add("p_IsRestoreMemberCard", registerCard.IsRestoreMemberCard);
                parameters.Add("p_FriendOpenId", registerCard.FriendOpenId);
                parameters.Add("p_ActiveTime", registerCard.ActiveTime);
                parameters.Add("error_code", null, DbType.Int32, ParameterDirection.Output);
                database.Execute("spRegisterWxUserCard", parameters, System.Data.CommandType.StoredProcedure, true);
                //error_code = -1000 表示用户信息稍微登记
                if (parameters.Get<int>("error_code") > 0)
                    throw new SharingException(string.Format("Error occured on proccess get card coupon. error code:{0}",
                        parameters.Get<int>("error_code")));
            }
        }

      
    }
}
