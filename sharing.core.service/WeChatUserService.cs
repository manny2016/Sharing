﻿

namespace Sharing.Core.Services {
	using System;
	using Sharing.Core.Models;
	using Sharing.Core;
	using Dapper;
	using System.Collections.Generic;
	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.DependencyInjection;
	using System.Linq;
	using System.Data;
	using System.Data.SqlClient;
	using Microsoft.SqlServer.Server;

	public class WeChatUserService : IWeChatUserService {
		private readonly IMemoryCache cache;
		private readonly IDatabaseFactory databaseFactory;
		public WeChatUserService(IMemoryCache cache,IDatabaseFactory databaseFactory) {
			this.cache = cache;
			this.databaseFactory = databaseFactory;
		}
		public Membership Register(RegisterWxUserContext context) {
			using ( var database = this.databaseFactory.GenerateDatabase(isWriteOnly: true) ) {

				var queryString = @"[dbo].[spRegisterWeChatUser]";
				var parameters = new IDbDataParameter[] {
					new SqlParameter() {
						SqlDbType = SqlDbType.Structured,
						TypeName = "[dbo].[RegisterWeChatUserStructure]",
						Value = new SqlDataRecord[] { Unity.Convert(context) },
						ParameterName = "@wxuser"
					}
				};
				database.Execute(queryString, parameters, System.Data.CommandType.StoredProcedure);
				queryString =
@"SELECT [src].*, [app].[MerchantId] FROM (
	SELECT [user].[Id],[identity].[AppId],[user].[UnionId],[identity].[OpenId],[Mobile],
	(SELECT SUM([RewardMoney])  FROM [dbo].[RewardLogging]  [reward] WHERE [reward].[WxUserId]= [user].[Id] ) AS [RewardMoney]
	FROM [dbo].[WxUser]  [user]
	LEFT JOIN  [dbo].[WxUserIdentity] [identity]
		ON [identity].[WxUserId] =[user].[Id]
	WHERE [identity].[AppId] = @appid and [identity].[OpenId]  = @openid
) [src]
LEFT JOIN  [dbo].[MWeChatApp] [app]
	ON [src].[AppId]= [app].[AppId]";
				return database.SqlQuerySingleOrDefault<Membership>(queryString,
					new {
						@appid = context.WxApp.AppId,
						@openid = context.Info.OpenId
					});

			}
		}

		public IList<ISharedContext> GetSharedContext(IMchId mch) {
			var cacheKey = string.Format("pyramid_{0}", mch.MerchantId);
			return this.cache.GetOrCreate<IList<ISharedContext>>(cacheKey, (entity) => {
				entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
				using ( var database = this.databaseFactory.GenerateDatabase(isWriteOnly: false) ) {
					var queryString = @"
SELECT [WxUserId] AS Id,[MerchantId],[InvitedBy] FROM [dbo].[SharedPyramid] (NOLOCK) 
WHERE [MerchantId]=@merchantId";
					return database.SqlQuery<SharedContext>(queryString, new { @merchantId = mch.MerchantId })
					.ToList<ISharedContext>();
				}
			});
		}


		

		public void RegisterCardCoupon(RegisterCardCoupon registerCard) {

			using ( var database = this.databaseFactory.GenerateDatabase(isWriteOnly: true) ) {
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
				if ( parameters.Get<int>("error_code") > 0 )
					throw new SharingException(string.Format("Error occured on proccess get card coupon. error code:{0}",
						parameters.Get<int>("error_code")));
			}
		}


	}
}
