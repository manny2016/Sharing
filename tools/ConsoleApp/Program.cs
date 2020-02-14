using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sharing.WeChat.Models;
using System.Threading;
using Sharing.Core.Models;
using Sharing.Core.Services;
using Sharing.Core.CMQ;
using System.Linq;
namespace ConsoleApp {
	class Program {
		static ServiceCollection collection = new ServiceCollection();
		static void Main(string[] args) {

			var naicha = new Specification[] {
				new Specification(){
					 Selected =0,
					 Name ="规格",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							  Name = "标杯",
							  Price = 0,
							   IsDefault = true,
						 },
						 new SpecificationSettings(){
							 Name = "大杯",
							 Price =200,
						 }
					 }
				},
				new Specification(){
					 Selected =0,
					 Name ="温度",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							  Name = "常温",
							  Price = 0,
							  IsDefault = true
						 },
						 new SpecificationSettings(){
							  Name = "温热",
							  Price = 0,
						 },
						 new SpecificationSettings(){
							 Name = "去冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "少冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "多冰",
							 Price =0,
						 }
					 }
				},

			};
			var ziyun = new Specification[] {
				new Specification(){
					 Selected =0,
					 Name ="规格",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							 Name = "大杯",
							 Price =0,
							 IsDefault  = true,
						 }
					 }
				},
				new Specification(){
					 Selected =0,
					 Name ="温度",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							  Name = "常温",
							  Price = 0,
							  IsDefault = true
						 },
						 new SpecificationSettings(){
							  Name = "温热",
							  Price = 0,
						 },
						 new SpecificationSettings(){
							 Name = "去冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "少冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "多冰",
							 Price =0,
						 }
					 }
				}
			};
			var pangpang = new Specification[] {
				new Specification(){
					 Selected =0,
					 Name ="规格",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							 Name = "标杯",
							 Price =0,
							 IsDefault =true,
						 }
					 }
				},
				new Specification(){
					 Selected =0,
					 Name ="温度",
					 Options = new SpecificationSettings[]{
						 new SpecificationSettings(){
							  Name = "常温",
							  Price = 0,
							  IsDefault = true
						 },
						 new SpecificationSettings(){
							  Name = "温热",
							  Price = 0,
						 },
						 new SpecificationSettings(){
							 Name = "去冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "少冰",
							 Price =0,
						 },
						 new SpecificationSettings(){
							 Name = "多冰",
							 Price =0,
						 }
					 }
				}

			};
			var products = new ProductModel[] {
				GenernateProductModel(1,"椰果奶茶",1,800,naicha),
				GenernateProductModel(2,"红豆奶茶",1,800,naicha),
				GenernateProductModel(3,"布丁奶茶",1,800,naicha),
				GenernateProductModel(4,"双拼奶茶",1,800,naicha),
				GenernateProductModel(5,"全家福奶茶",1,800,naicha),
				GenernateProductModel(6,"奥利奥波波茶",1,800,naicha),
				GenernateProductModel(7,"芝士奶茶",1,800,naicha),

				GenernateProductModel(8,"茉香绿茶",2,1200,ziyun),
				GenernateProductModel(9,"蜜桃乌龙",2,1200,ziyun),
				GenernateProductModel(10,"四季春茶",2,1200,ziyun),

				GenernateProductModel(11,"紫薯牛乳茶",3,1400,pangpang),
				GenernateProductModel(12,"芒果牛乳茶",3,1200,pangpang),
				GenernateProductModel(13,"草莓牛乳茶",3,1200,pangpang),
				GenernateProductModel(14,"巧克力奶茶",3,1200,pangpang),
				GenernateProductModel(15,"宇治抹茶",3,1200,pangpang),
				GenernateProductModel(16,"芒果奶昔",3,1500,pangpang),
				GenernateProductModel(17,"火龙果奶昔",3,1500,pangpang),

				GenernateProductModel(18,"瘦身梅子茶",4,1200,ziyun),
				GenernateProductModel(19,"蜂蜜柚子茶",5,1300,ziyun),
				GenernateProductModel(20,"百香果金桔水果茶",4,1600,ziyun),
				GenernateProductModel(21,"满杯橙子",4,1600,ziyun),
				GenernateProductModel(22,"霸气果茶",4,1800,ziyun),

				GenernateProductModel(23,"柠檬红茶",5,1000,ziyun),
				GenernateProductModel(24,"炸弹柠檬",5,1200,ziyun),
				GenernateProductModel(25,"酸爽柠檬优多",5,1200,ziyun),
				GenernateProductModel(26,"柠檬薄荷蜜",5,1300,ziyun),
				GenernateProductModel(27,"金桔晶冻柠檬",5,1400,ziyun),
				GenernateProductModel(28,"柠檬西柚多多",5,1400,ziyun),

				GenernateProductModel(29,"柠檬双玫起苏泡泡",6,1300,ziyun),
				GenernateProductModel(30,"桃气柠檬公主多芽泡泡",6,1200,ziyun),
				GenernateProductModel(31,"柠檬血橙微量元素泡泡",6,1200,ziyun),
				GenernateProductModel(32,"柠檬柚子薄荷泡泡",6,1400,ziyun),
			};
			var queryString = @"MERGE INTO [dbo].[Product] AS [target]
USING(
	VALUES
	{0}
) AS [source]([Id],[MerchantId],[CategoryId],[Name],[Price],[SalesVol],[SortNo],[Enabled],[Description],[ImageUrl],[Settings],[CreatedBy],[CreatedDateTime],[LastUpdatedBy],[LastUpdatedDateTime])
ON [target].[Id] = [source].[Id] AND [target].[MerchantId] = [source].[MerchantId]
WHEN MATCHED THEN UPDATE SET 	
[target].[CategoryId]	= [source].[CategoryId]
,[target].[Name]		= [source].[Name]
,[target].[Price]		= [source].[Price]
,[target].[SalesVol]	= [source].[SalesVol]
,[target].[SortNo]		= [source].[SortNo]
,[target].[Enabled]		= [source].[Enabled]
,[target].[Description]	= [source].[Description]
,[target].[ImageUrl]	= [source].[ImageUrl]
,[target].[LastUpdatedBy]=[source].[LastUpdatedBy]
,[target].[LastUpdatedDateTime]	=[source].[LastUpdatedDateTime]
WHEN NOT MATCHED THEN INSERT
([Id],[MerchantId],[CategoryId],[Name],[Price],[SalesVol],[SortNo],[Enabled],[Description],[ImageUrl],[CreatedBy],[CreatedDateTime])
VALUES([source].[Id] ,[source].[MerchantId] ,[source].[CategoryId] ,[source].[Name] ,[source].[Price] ,[source].[SalesVol] ,[source].[SortNo] ,[source].[Enabled] ,[source].[Description] ,[source].[ImageUrl] ,[source].[CreatedBy] ,[source].[CreatedDateTime] );
";
			queryString = string.Format(queryString, string.Join(",\r\n", products.Select((ctx) => {
				return string.Format("({0})", string.Join(",", new string[]{
					ctx.Id.ToString(),
					ctx.MerchantId.ToString(),
					ctx.CategoryId.ToString(),
					$"'{ctx.Name}'",
					ctx.Price.ToString(),
					ctx.SalesVol.ToString(),
					ctx.SortNo.ToString(),
					(ctx.Enabled?1:0).ToString(),
					"''",
					$"'{ctx.ImageUrl}'",
					$"'{ctx.Options}'",
					"'Initialization'",
					"DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())",
					"'Initialization'",
					"DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())"
				}));
			})));
		}

		static ProductModel GenernateProductModel(int id, string productName, int categoryId, int defaultPrice, Specification[] specifications) {
			return new ProductModel() {
				Name = productName,
				Id = id,
				MerchantId = 1,
				CategoryId = categoryId,
				Price = defaultPrice,
				ImageUrl = string.Empty,
				SalesVol = 0,
				Options = (new {
					banners = new string[] { },
					specifications = specifications
				}).SerializeToJson(),
				SortNo = 1,
				Enabled = true
			};
		}
	}
}
