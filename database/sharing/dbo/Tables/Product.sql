CREATE TABLE [dbo].[Product]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_ProductId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_Product_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[CategoryId] BIGINT NOT NULL CONSTRAINT [FK_Product_CategoryId] FOREIGN KEY REFERENCES [dbo].[Category] ([Id]),
    [Name]   NVARCHAR(32) NOT NULL,
	[Price] INT NOT NULL,
	[SalesVol] INT,
	[SortNo] INT,
	[Enabled] BIT DEFAULT(1),
	[Description] NVARCHAR(200),
	[ImageUrl] NVARCHAR(2000),
	[CreatedDateTime] BIGINT NULL, 
    [LastUpdatedTime] BIGINT NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL,
	[ScenarioId] UNIQUEIDENTIFIER NULL
        
       
)
