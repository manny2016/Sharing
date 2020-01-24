CREATE TABLE [dbo].[WxUser]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_WxUserId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_WxUser_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[UnionId] NVARCHAR(32),
	[InvitedBy] BIGINT NULL CONSTRAINT [FK_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),	
    [Mobile]   NVARCHAR(11) NULL,
	[RegistrySource] INT,
	[NickName] NVARCHAR(50),
	[Country] NVARCHAR(50),
	[Province] NVARCHAR(50),
	[City] NVARCHAR(50),
	[AvatarUrl] NVARCHAR(200),
	[AppId] NVARCHAR(32),
	[OpenId] NVARCHAR(32),
	[CreatedDateTime] BIGINT NULL, 
    [LastUpdatedTime] BIGINT NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL ,
	[ScenarioId] UNIQUEIDENTIFIER NULL
	
)
