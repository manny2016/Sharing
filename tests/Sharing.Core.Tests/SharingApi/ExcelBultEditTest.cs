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
using System.IO;
using Sharing.Core.Models.Excel;
using Sharing.Core.Tests.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Sharing.Core.Tests.SharingApi {
	[TestClass]
	public class ExcelBultEditTest {

		[TestMethod]
		public void Write() {
			var array = new string[] { };
			var name = array.GetType().Name;
			var helper = IoC.GetService<IExcelBulkEditHelper>();
			var model = new ExcelDataModel<CategroyExcelBulkRow, ProductExcelBulkRow, ProductOptionExcelBultEditRow>();
			using ( var stream = new FileStream($"{DateTime.Now.ToUnixStampDateTime()}.xlsx", FileMode.Create) ) {
				
				using ( var database = SharingConfigurations.GenerateDatabase() ) {
					model.Data1 = database.SqlQuery<CategroyExcelBulkRow>("SELECT * FROM [dbo].[Category] (NOLOCK)").ToArray();
					model.Data2 = database.SqlQuery<ProductExcelBulkRow>("SELECT * FROM [dbo].[Product] (NOLOCK)").ToArray();
					model.Data3 = model.Data2.SelectMany((ctx) => {
						return ctx.Settings.DeserializeToObject<ProductSettings>()
						?.Specifications
						.SelectMany((option) => {
							return option.Options.Select(x => new ProductOptionExcelBultEditRow() {
								Name = option.Name,
								Price = x.Price,
								Item = x.Name,
								Product = ctx.Name,
								ProductId = ctx.Id
							});
						});
					});
				}
				model.DataMark = new ExcelDataMark() {
					DateTime = DateTime.Now.ToUnixStampDateTime(),
					Additional = new Dictionary<string, string>(),
					Version = new Version("1.0.0")
				};

				helper.Write(stream, model);
				stream.Flush();
			}
		}
		[TestInitialize]
		public void TestInitialize() {

			IoC.ConfigureService(null, (configure) => {
				configure.AddLogging();
				configure.AddWeChatApiService();
				configure.AddRandomGenerator();
				configure.AddSharingHostService();
				configure.AddWeChatPayService();
				configure.AddWeChatUserService();
				configure.AddMcardService();
				configure.AddMemoryCache();
				configure.AddWeChatMsgHandler();
				configure.AddExcelBulExittHelper();
				configure.ConfigureAppConfiguration();
				

			});

		}
	}
}
