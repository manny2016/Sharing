CREATE TABLE [dbo].[MShop]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_MShopId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_MShop_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
    [WxShopId]  BIGINT,
	[ShopName] NVARCHAR(100),
	[Address] NVARCHAR(200),
	[CreatedBy] NVARCHAR(50) NULL, 
	[CreatedDateTime] BIGINT NULL,         
	[LastUpdatedBy] NVARCHAR(50) NULL,
	[LastUpdatedDateTime] BIGINT NULL, 
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
