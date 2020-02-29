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

		private IDatabaseFactory factory;

		[TestMethod]
		public void Write() {
			var array = new string[] { };
			var name = array.GetType().Name;

			var helper = new DefaultExcelBulkEditHelper();
			var model = new ExcelDataModel<CategroyExcelBulkRow, ProductExcelBulkRow>();
			using ( var stream = new FileStream($"{DateTime.Now.ToUnixStampDateTime()}.xlsx", FileMode.Create) ) {

				using ( var database = factory.GenerateDatabase() ) {
					model.Data1 = database.SqlQuery<CategroyExcelBulkRow>("SELECT * FROM [dbo].[Category] (NOLOCK)").ToArray();
					model.Data2 = database.SqlQuery<ProductExcelBulkRow>("SELECT * FROM [dbo].[Product] (NOLOCK)").ToArray();
					model.Data2.ToList().ForEach(ctx => {
						var settings = ctx.Settings.DeserializeToObject<ProductSettings>();
						ctx.Options = settings?.Specifications.SerializeToJson();
						ctx.Banners = string.Join(",",
							(settings?.Banners.Select(x => x.Replace("https://www.yourc.club/images/products/", string.Empty))) ??
							new string[] { ctx.ImageUrl.Replace("https://www.yourc.club/images/products/", string.Empty) });
						ctx.Size = settings?.Specifications[0].SerializeToJson();
						ctx.Temperature = settings?.Specifications[1].SerializeToJson();
						ctx.ImageUrl = ctx.ImageUrl?.Replace("https://www.yourc.club/images/products/", string.Empty);
						ctx.Banners = string.IsNullOrEmpty(ctx.Banners) ? ctx.ImageUrl : ctx.Banners;
					});
				}
				model.DataMark = new ExcelDataMark() {
					DateTime = DateTime.Now.ToUnixStampDateTime(),
					Additional = new Dictionary<string, string>(),
					Version = "1.0"
				};

				helper.Write(stream, model);
				stream.Flush();
			}
		}
		[TestMethod]
		public void Read() {

		}
		[TestMethod]
		public void ImportProducts() {
			using ( var stream = new FileStream("Products.xlsx", FileMode.Open, FileAccess.Read) ) {
				var helper = new DefaultExcelBulkEditHelper();
				var categories = helper.Read<CategroyExcelBulkRow>(stream);
				var products = helper.Read<ProductExcelBulkRow>(stream);

				var queryString = @"
SET IDENTITY_INSERT [dbo].[Category] ON
MERGE INTO [dbo].[Category] [target]
USING(
	VALUES
	{0}
)[source]([Id],[MerchantId],[Name],[Enabled],[Description],[JsonString],[CreatedDateTime],[LastUpdatedDateTime],[CreatedBy],[LastUpdatedBy])
ON [target].[Id] = [source].[Id] AND [target].[MerchantId] = [source].[MerchantId]
WHEN MATCHED THEN UPDATE SET
	[target].[Name]		= [source].[Name],
	[target].[Enabled]	= [source].[Enabled],
	[target].[Description]	= [source].[Description],
	[target].[JsonString]	= [source].[JsonString],	
	[target].[LastUpdatedDateTime]	= [source].[LastUpdatedDateTime],
	[target].[LastUpdatedBy]	= [source].[LastUpdatedBy]
WHEN NOT MATCHED THEN INSERT ([Id],[MerchantId],[Name],[Enabled],[Description],[JsonString],[CreatedDateTime],[CreatedBy])
	VALUES([source].[Id],[source].[MerchantId],[source].[Name],[source].[Enabled],[source].[Description],[source].[JsonString],[source].[CreatedDateTime],[source].[CreatedBy]);
SET IDENTITY_INSERT [dbo].[Category] OFF
";
				using ( var database = factory.GenerateDatabase() ) {
					queryString = string.Format(queryString, string.Join(",", categories.Data.Where(x => x.Id != 0).Select(ctx => {
						return $"\r\n({ctx.Id},{ctx.MerchantId},'{ctx.Name}',1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization')";
					})));
					database.Execute(queryString);
				}

				queryString = @"SET IDENTITY_INSERT [dbo].[Product] ON
MERGE INTO [dbo].[Product] AS [target]
USING(
	VALUES
	{0}
				
) AS[source]([Id],[MerchantId],[CategoryId],[Name],[Price],[SalesVol],[SortNo],[Enabled],[Description],[ImageUrl],[Settings],[CreatedBy],[CreatedDateTime],[LastUpdatedBy],[LastUpdatedDateTime])
ON[target].[Id] = [source].[Id] AND[target].[MerchantId] = [source].[MerchantId]
		WHEN MATCHED THEN UPDATE SET
[target].[CategoryId]	= [source].[CategoryId]
,[target].[Name]		= [source].[Name]
,[target].[Price]		= [source].[Price]
,[target].[SalesVol]	= [source].[SalesVol]
,[target].[SortNo]		= [source].[SortNo]
,[target].[Enabled]		= [source].[Enabled]
,[target].[Description]	= [source].[Description]
,[target].[ImageUrl]	= [source].[ImageUrl]
,[target].[Settings]	= [source].[Settings]
,[target].[LastUpdatedBy]=[source].[LastUpdatedBy]
,[target].[LastUpdatedDateTime]	=[source].[LastUpdatedDateTime]
		WHEN NOT MATCHED THEN INSERT
		([Id], [MerchantId], [CategoryId], [Name], [Price], [SalesVol], [SortNo], [Enabled], [Description], [ImageUrl], [CreatedBy], [CreatedDateTime])
VALUES([source].[Id] , [source].[MerchantId] , [source].[CategoryId] , [source].[Name] , [source].[Price] , [source].[SalesVol] , [source].[SortNo] , [source].[Enabled] , [source].[Description] , [source].[ImageUrl] , [source].[CreatedBy] , [source].[CreatedDateTime] );

		SET IDENTITY_INSERT[dbo].[Product] OFF

";
				using ( var database = factory.GenerateDatabase() ) {
					queryString = string.Format(queryString, string.Join(",", products.Data.Where(x => x.Id != 0).Select(ctx => {
						var settings = new ProductSettings();
						settings.Banners = ctx.Banners.Split(",")
						?.Select(x => $"https://www.yourc.club/images/products/{x}")
						.ToArray();

						settings.Specifications = new List<Specification>(){
							ctx.Size.DeserializeToObject<Specification>(),
							ctx.Temperature.DeserializeToObject<Specification>()
						};
						
						ctx.ImageUrl = string.IsNullOrEmpty(ctx.ImageUrl) ? string.Empty : $"https://www.yourc.club/images/products/{ctx.ImageUrl}";
						return $"\r\n({ctx.Id},{ctx.MerchantId},{ctx.CategoryId},'{ctx.Name}',{ctx.Price},{ctx.SalesVol},{ctx.SortNo},1,'{ctx.Description}','{ctx.ImageUrl}','{settings.SerializeToJson()}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()))";
					})));
					database.Execute(queryString);
				}
			}
		}
		[TestInitialize]
		public void TestInitialize() {

			var builder = new ConfigurationBuilder();
			var configuration = new ConfigurationRoot(new List<IConfigurationProvider>() {
				new  JsonConfigurationProvider(new JsonConfigurationSource(){
					 Optional = true,
					 FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.Environment.CurrentDirectory),
					 Path ="appsettings.Development.json",
					 ReloadOnChange  =true
				})
			});
			builder.Build();
			factory = new DatabaseFactory(configuration);
		}
	}
}
