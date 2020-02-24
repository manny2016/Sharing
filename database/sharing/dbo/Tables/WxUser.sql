CREATE TABLE [dbo].[WxUser]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_WxUserId] PRIMARY KEY,	
	[UnionId] NVARCHAR(32),
	--[AppId] NVARCHAR(32),
	--[OpenId] NVARCHAR(32),
	[InvitedBy] BIGINT NULL CONSTRAINT [FK_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),	
    [Mobile]   NVARCHAR(11) NULL,
	[RegistrySource] INT,
	[NickName] NVARCHAR(50),
	[Country] NVARCHAR(50),
	[Province] NVARCHAR(50),
	[City] NVARCHAR(50),
	[AvatarUrl] NVARCHAR(200),
	[Shared] BIT DEFAULT(0),
	[CreatedBy] NVARCHAR(50) NULL, 
	[CreatedDateTime] BIGINT NULL,         
	[LastUpdatedBy] NVARCHAR(50) NULL,
	[LastUpdatedDateTime] BIGINT NULL, 
	[ScenarioId] UNIQUEIDENTIFIER NULL
	
)
