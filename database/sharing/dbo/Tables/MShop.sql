CREATE TABLE [dbo].[MShop]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_MShopId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_MShop_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
    [WxShopId]  BIGINT,
	[ShopName] NVARCHAR(100),
	[Address] NVARCHAR(200),
	[CreatedDateTime] BIGINT NULL, 
    [LastUpdatedTime] BIGINT NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL,
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
