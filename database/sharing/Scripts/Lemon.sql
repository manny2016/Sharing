SET IDENTITY_INSERT [dbo].[Merchant] ON
MERGE INTO [dbo].[Merchant] [target]
USING(
	VALUES
	(1,'92511402MA6941EG0R','柠檬工坊东坡里店','','',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization')
) AS [source]([Id],[MCode],[BrandName],[LogoUrl],[Address],[CreatedDateTime],[LastUpdatedDateTime], [CreatedBy],[LastUpdatedBy])
ON [target].[Id] = [source].[Id]
WHEN MATCHED THEN UPDATE SET 
	[target].[MCode] = [source].[MCode],
	[target].[BrandName] = [source].[BrandName],
	[target].[LogoUrl] = [source].[LogoUrl],
	[target].[Address] = [source].[Address],
	[target].[LastUpdatedDateTime] = [source].[LastUpdatedDateTime],
	[target].[LastUpdatedBy] = [source].[LastUpdatedBy]
WHEN NOT MATCHED THEN INSERT([Id],[MCode],[BrandName],[LogoUrl],[Address],[CreatedDateTime],[CreatedBy])
	VALUES([source].[Id],[source].[MCode],[source].[BrandName],[source].[LogoUrl],[source].[Address],[source].[CreatedDateTime],[source].[CreatedBy]);


SET IDENTITY_INSERT [dbo].[Merchant] OFF

MERGE INTO [dbo].[MWeChatApp] [target]
USING(
	VALUES
	(1,2,'wx20da9548445a2ca7','c746db3e48b79d474806ba983b07257c','gh_085392ac0d21','{"MchId":1520961881,"PayKey":"f5b34b2750ee46318c80047902d5484c"}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
	(1,1,'wx6a15c5888e292f99','a0263dcb8a0fc55485cea4d641fcc2c5','gh_db637ae33750','{"MchId":1520961881,"PayKey":"f5b34b2750ee46318c80047902d5484c"}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()))
) AS [source]([MerchantId],[AppType],[AppId],[Secret],[OriginalId],[Payment],[CreatedBy],[CreatedDateTime],[LastUpdatedBy],[LastUpdatedDateTime])
ON [target].[MerchantId] = [source].[MerchantId] AND [target].[AppId] = [source].[AppId]
AND [target].[AppType] = [source].[AppType]
WHEN MATCHED THEN UPDATE SET 
	[target].[Secret] = [source].[Secret],
	[target].[OriginalId] = [source].[OriginalId],
	[target].[Payment] = [source].[Payment],
	[target].[LastUpdatedBy] = [source].[LastUpdatedBy],
	[target].[LastUpdatedDateTime] = [source].[LastUpdatedDateTime]
WHEN NOT MATCHED THEN INSERT([MerchantId],[AppType],[AppId],[Secret],[OriginalId],[Payment],[CreatedBy],[CreatedDateTime])
	VALUES([source].[MerchantId],[source].[AppType],[source].[AppId],[source].[Secret],[source].[OriginalId],[source].[Payment],[source].[CreatedBy],[source].[CreatedDateTime]);


SET IDENTITY_INSERT [dbo].[Category] ON
MERGE INTO [dbo].[Category] [target]
USING(
	VALUES
	(1,1,'奶茶系列',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),
	(2,1,'芝云茗茶',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),
	(3,1,'胖胖杯',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),
	(4,1,'果茶系列',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),
	(5,1,'柠檬系列',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),
	(6,1,'气泡系列',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization'),	
	(7,1,'小吃系列',		1,'','[]',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization','Initialization')
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

SET IDENTITY_INSERT [dbo].[Product] ON
MERGE INTO [dbo].[Product] AS [target]
USING(
	VALUES
	(1,1,1,'椰果奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(2,1,1,'红豆奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(3,1,1,'布丁奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(4,1,1,'双拼奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(5,1,1,'全家福奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(6,1,1,'奥利奥波波茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(7,1,1,'芝士奶茶',800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true},{"name":"大杯","price":200,"isDefault":false}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(8,1,2,'茉香绿茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(9,1,2,'蜜桃乌龙',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(10,1,2,'四季春茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(11,1,3,'紫薯牛乳茶',1400,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(12,1,3,'芒果牛乳茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(13,1,3,'草莓牛乳茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(14,1,3,'巧克力奶茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(15,1,3,'宇治抹茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(16,1,3,'芒果奶昔',1500,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(17,1,3,'火龙果奶昔',1500,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"标杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(18,1,4,'瘦身梅子茶',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(19,1,5,'蜂蜜柚子茶',1300,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(20,1,4,'百香果金桔水果茶',1600,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(21,1,4,'满杯橙子',1600,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(22,1,4,'霸气果茶',1800,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(23,1,5,'柠檬红茶',1000,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(24,1,5,'炸弹柠檬',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(25,1,5,'酸爽柠檬优多',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(26,1,5,'柠檬薄荷蜜',1300,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(27,1,5,'金桔晶冻柠檬',1400,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(28,1,5,'柠檬西柚多多',1400,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(29,1,6,'柠檬双玫起苏泡泡',1300,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(30,1,6,'桃气柠檬公主多芽泡泡',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(31,1,6,'柠檬血橙微量元素泡泡',1200,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())),
(32,1,6,'柠檬柚子薄荷泡泡',1400,0,1,1,'','','{"banners":[],"specifications":[{"name":"规格","options":[{"name":"大杯","price":0,"isDefault":true}],"selected":0},{"name":"温度","options":[{"name":"常温","price":0,"isDefault":true},{"name":"温热","price":0,"isDefault":false},{"name":"去冰","price":0,"isDefault":false},{"name":"少冰","price":0,"isDefault":false},{"name":"多冰","price":0,"isDefault":false}],"selected":0}]}','Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'Initialization',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()))
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
,[target].[Settings]	= [source].[Settings]
,[target].[LastUpdatedBy]=[source].[LastUpdatedBy]
,[target].[LastUpdatedDateTime]	=[source].[LastUpdatedDateTime]
WHEN NOT MATCHED THEN INSERT
([Id],[MerchantId],[CategoryId],[Name],[Price],[SalesVol],[SortNo],[Enabled],[Description],[ImageUrl],[CreatedBy],[CreatedDateTime])
VALUES([source].[Id] ,[source].[MerchantId] ,[source].[CategoryId] ,[source].[Name] ,[source].[Price] ,[source].[SalesVol] ,[source].[SortNo] ,[source].[Enabled] ,[source].[Description] ,[source].[ImageUrl] ,[source].[CreatedBy] ,[source].[CreatedDateTime] );


SET IDENTITY_INSERT [dbo].[Product] OFF