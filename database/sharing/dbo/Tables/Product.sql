CREATE TABLE [dbo].[Product]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_ProductId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_Product_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[CategoryId] BIGINT NOT NULL CONSTRAINT [FK_Product_CategoryId] FOREIGN KEY REFERENCES [dbo].[Category] ([Id]),
    [Name]   NVARCHAR(32) NOT NULL CONSTRAINT [UK_Product_Name_MerchantId_CategoryId] UNIQUE ([Name],[MerchantId],[CategoryId]),
	[Price] INT NOT NULL,
	[SalesVol] INT,
	[SortNo] INT,
	[Enabled] BIT DEFAULT(1),
	[Description] NVARCHAR(200),
	[ImageUrl] NVARCHAR(2000),
	[Settings] NVARCHAR(MAX),
	[CreatedBy] NVARCHAR(50) NULL, 
	[CreatedDateTime] BIGINT NULL,         
	[LastUpdatedBy] NVARCHAR(50) NULL,
	[LastUpdatedDateTime] BIGINT NULL, 
	[ScenarioId] UNIQUEIDENTIFIER NULL
        
       
)
