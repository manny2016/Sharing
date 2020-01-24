CREATE TABLE [dbo].[Merchant]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_MerchantId] PRIMARY KEY,
	[MCode] NVARCHAR(32) NOT NULL,
	[BrandName] NVARCHAR(100) NOT NULL,
	[LogoUrl] NVARCHAR(100) NULL,
	[Address] NVARCHAR(100) NULL, 
    [CreatedDateTime] BIGINT NULL, 
    [LastUpdatedTime] BIGINT NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL,
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
