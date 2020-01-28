

using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core;
using Sharing.Core.Models;
using Sharing.WeChat.Models;
using Microsoft.Extensions.DependencyInjection;
using Sharing.Core.Services;
using System.Linq;
namespace Sharing.Core.Tests {
	[TestClass]
	public class DatabaseTest {

		private readonly Guid scenarioId = Guid.NewGuid();
		private readonly string AppId = "e999292b01fa4258b6ead9ec8bf27d2e";
		private readonly string mCode = "5cafd148bde3466fa586bae1ddb3d50b";
		private readonly string originalId = Guid.NewGuid().ToString().Replace("-", string.Empty);
		private readonly string openid = Guid.NewGuid().ToString().Replace("-", string.Empty);
		[TestInitialize]
		public void Initialize() {
			IoC.ConfigureServiceProvider(null, (configure) => {
				configure.AddLogging();
				configure.AddWeChatApiService();
				configure.AddRandomGenerator();
				configure.AddSharingHostService();
				configure.AddWeChatPayService();
				configure.AddWeChatUserService();
				configure.AddMcardService();
				configure.AddMemoryCache();
				configure.AddWeChatMsgHandler();
				configure.Add(new ServiceDescriptor(typeof(System.Configuration.Configuration),
					ConfigurationManager.OpenExeConfiguration("Sharing.Core.Tests.dll")));

			});
			using ( var database = SharingConfigurations.GenerateDatabase() ) {
				database.Execute(@"
IF NOT EXISTS (SELECT Id FROM [dbo].[Merchant] WHERE MCode =@MCode)
BEGIN
	INSERT INTO [dbo].[Merchant]
	([MCode],[BrandName],[LogoUrl],[Address],[CreatedDateTime],[LastUpdatedTime],[CreatedBy],[LastUpdatedBy],[ScenarioId])
	VALUES(@MCode,@BrandName,@LogoUrl,@Address,@CreatedDateTime,@LastUpdatedTime,@CreatedBy,@LastUpdatedBy,@ScenarioId);
END;",
new {
	@MCode = mCode,
	@BrandName = "柠檬工坊东坡里店",
	@LogoUrl = "https://www.baidu.com/index.html",
	@Address = "眉山市东坡里商业水街",
	@CreatedDateTime = DateTime.UtcNow.ToUnixStampDateTime(),
	@LastUpdatedTime = DateTime.UtcNow.ToUnixStampDateTime(),
	@CreatedBy = "System",
	@LastUpdatedBy = "System",
	@ScenarioId = scenarioId
});
				database.Execute(@"
IF NOT EXISTS (
	SELECT [app].[MerchantId] FROM [dbo].[MWeChatApp] [app]
	LEFT JOIN [dbo].[Merchant] [merchant] ON  [app].[MerchantId] = [merchant].[Id]
	WHERE [merchant].[MCode] =@mCode
)
BEGIN
INSERT INTO [dbo].[MWeChatApp]
([MerchantId],[AppType],[AppId],[Secret],[OriginalId],[Payment],[CreatedDateTime],[LastUpdatedDateTime],[CreatedBy]
,[LastUpdatedBy],[ScenarioId])
VALUES((SELECT TOP 1 Id FROM [dbo].[Merchant] WHERE MCode=@mCode),@AppType,@AppId,@Secret,@OriginalId,@Payment,@CreatedDateTime,@LastUpdatedDateTime,@CreatedBy,@LastUpdatedBy,@ScenarioId )
END",
new {
	@mCode = mCode,
	@AppType = (int)AppTypes.Miniprogram,
	@AppId = AppId,
	@Secret = "abc",
	@OriginalId = originalId,
	@Payment = "{}",
	@CreatedDateTime = DateTime.UtcNow.ToUnixStampDateTime(),
	@LastUpdatedDateTime = DateTime.UtcNow.ToUnixStampDateTime(),
	@CreatedBy = "System",
	@LastUpdatedBy = "System",
	@ScenarioId = scenarioId
});
			}

		}
		#region IWeChatUserService 
		[TestMethod]
		public void Register() {

			var service = IoC.GetService<IWeChatUserService>();
			var result = service.Register(new RegisterWxUserContext() {
				AppType = AppTypes.Miniprogram,
				ScenarioId = scenarioId,
				Info = new WeChatUserInfo() {
					AvatarUrl = "http://www.baidu.com/html",
					City = "眉山市",
					Country = "中国",
					Gender = "男",
					NickName = "稻草人",
					OpenId = openid,
					Province = "四川省",
					Subscribe = true,
					UnionId = openid,
				},
				WxApp = new WxApp() {
					AppId = AppId,
					AppType = AppTypes.Miniprogram,
					OriginalId = "xxx",
					Secret = "xxx"
				}
			});
			Assert.AreEqual(string.IsNullOrEmpty(result.AppId), false);
			Assert.AreEqual(result.MerchantId == null, false);
		}
		[TestMethod]
		public void UpgradeSharedPyramid() {
			var executeSqlString = @"spUpgradeSharedPyramid";
			using ( var database = SharingConfigurations.GenerateDatabase(isWriteOnly: true) ) {
				var parameters = new Dapper.DynamicParameters();
				parameters.Add("@sharedByOpenId", "0377f35bd3a34d90bc2dd5731126fc0b", System.Data.DbType.String);
				parameters.Add("@currentOpenId", "78c629db3c67433e992c4f92d168a0bc", System.Data.DbType.String);
				parameters.Add("@appid", "e999292b01fa4258b6ead9ec8bf27d2e", System.Data.DbType.String);
				database.Execute(executeSqlString, parameters, System.Data.CommandType.StoredProcedure, true);
			}
		}
		[TestMethod]
		public void RegisterCardCoupon() {
			Assert.AreEqual(true, false);
		}
		[TestMethod]
		public void GetSharedContext() {
			var service = IoC.GetService<IWeChatUserService>();
			var result = service.GetSharedContext(new WxUserKey() { MerchantId = 42 });
			Assert.AreEqual(result.Count, result.Count);
		}

		#endregion
		#region 
		[TestMethod]
		public void PrepareUnifiedorderforTopup() {
			Assert.AreEqual(true, false);
		}
		[TestMethod]
		public void PrepareUnifiedorderforOnlineOrder() {
			var service = IoC.GetService<IWeChatPayService>();
			var result = service.PrepareUnifiedorder(new OrderContext() {
				Address = "xxx",
				Customer = "xx",
				Delivery = DeliveryTypes.BySelf,
				AppId = "e999292b01fa4258b6ead9ec8bf27d2e",
				Tel = "13961576298",
				Fare = "5",
				Details = new OrderDetail[] {
							new OrderDetail() {
								 Id  = 1, Name = "XX", Number = 1, Option = "XX,XX" , Price = "1000",
							}
					   },
				MerchantId = 42,
				Money = "100",
				OpenId = "0377f35bd3a34d90bc2dd5731126fc0b",
				Remarks = "xx"

			}, out WxPayAttach attach);

			Assert.AreNotEqual(result, null);
		}
		[TestMethod]
		public void GetPayment() {
			var service = IoC.GetService<IWeChatPayService>();
			var result = service.GetPayment("e999292b01fa4258b6ead9ec8bf27d2e");
			Assert.AreNotEqual(result, null);
		}
		[TestMethod]
		public void GetTradeByTradeId() {
			var service = IoC.GetService<IWeChatPayService>();
			var result = service.GetTradeByTradeId("T202001260000000001");
			Assert.AreNotEqual(result, null);
		}
		#endregion

		#region  ISharingHostService
		[TestMethod]
		public void GetProductTree() {
			var service = IoC.GetService<ISharingHostService>();

			Assert.AreEqual(service.GetProductTree(42).Length, 0);
		}
		[TestMethod]
		public void GetHotSaleProducts() {
			var service = IoC.GetService<ISharingHostService>();

			Assert.AreEqual(service.GetHotSaleProducts(42).Count(), 0);
		}
		[TestMethod]
		public void GetMerchantDetils() {
			var service = IoC.GetService<ISharingHostService>();
			Assert.AreEqual(service.MerchantDetails.Count, service.MerchantDetails.Count);
		}
		#endregion
		[TestCleanup]
		public void Cleanup() {
			using ( var database = SharingConfigurations.GenerateDatabase() ) {
				database.Execute("DELETE FROM [dbo].[RewardLogging] WHERE [ScenarioId] = @scenarioId", new {
					@scenarioId = scenarioId
				});
				database.Execute("DELETE FROM [dbo].[Trade] WHERE [ScenarioId] = @scenarioId", new {
					@scenarioId = scenarioId
				});
				database.Execute("DELETE FROM [dbo].[Product] WHERE [ScenarioId] = @scenarioId", new {
					@scenarioId = scenarioId
				});
				//database.Execute("DELETE FROM [dbo].[MWeChatApp] WHERE [ScenarioId] = @scenarioId", new {
				//	@scenarioId = scenarioId
				//});
				database.Execute("DELETE FROM [dbo].[Category] WHERE [ScenarioId] = @scenarioId", new {
					@scenarioId = scenarioId
				});
				//database.Execute("DELETE FROM [dbo].[Merchant] WHERE [ScenarioId] = @scenarioId", new {
				//	@scenarioId = scenarioId
				//});
				database.Execute("DELETE FROM [dbo].[MShop] WHERE [ScenarioId] = @scenarioId", new {
					scenarioId = scenarioId
				});
				//database.Execute("DELETE FROM [dbo].[WxUser] WHERE [ScenarioId] = @scenarioId", new {
				//	scenarioId = scenarioId
				//});

			}
		}
	}
}
