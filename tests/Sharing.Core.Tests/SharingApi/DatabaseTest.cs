

using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core;
using Sharing.Core.Models;
using Sharing.WeChat.Models;

namespace Sharing.Core.Tests {
	[TestClass]
	public class DatabaseTest {

		private readonly Guid scenarioId = Guid.NewGuid();
		[TestInitialize]
		public void Initialize() {

		}
		#region IWeChatUserService 
		[TestMethod]
		public void Register() {

		}
		[TestMethod]
		public void RegisterCardCoupon() {

		}
		[TestMethod]
		public void GetSharedContext() {

		}
		[TestMethod]
		public void GetWxUserId() {

		}
		#endregion
		#region 
		[TestMethod]
		public void PrepareUnifiedorderforTopup() {

		}
		[TestMethod]
		public void PrepareUnifiedorderforOnlineOrder() {

		}
		[TestMethod]
		public void GetPayment() {

		}
		[TestMethod]
		public void GetTradeByTradeId() {

		}
		#endregion

		#region  ISharingHostService

		public void GetProductTree() {

		}
		public void GetHotSaleProducts() {

		}
		#endregion
		[TestCleanup]
		public void Cleanup() {
			using ( var database = SharingConfigurations.GenerateDatabase(
				configuration: ConfigurationManager.OpenExeConfiguration("Sharing.Core.Tests.dll")) ) {

				database.Execute("DELETE FROM [dbo].[RewardLogging] WHERE [ScenarioId] = @scenarioId", new {
					scenarioId = scenarioId
				});
			}
		}
	}
}
