CREATE TABLE [dbo].[Category]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_CategoryId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_Category_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[Name] NVARCHAR(50) NOT NUll,
	[Enabled] BIT DEFAULT(1) NULL,
	[Description] NVARCHAR(200) NULL,
	[JsonString] NVARCHAR(MAX) DEFAULT('[]'), 
    [CreatedDateTime] BIGINT NULL, 
    [LastUpdatedDateTime] BIGINT NULL,
	[CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL,
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
