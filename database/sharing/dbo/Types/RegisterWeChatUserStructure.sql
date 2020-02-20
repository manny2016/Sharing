CREATE TYPE [dbo].[RegisterWeChatUserStructure] AS TABLE
(
	[UnionId]			NVARCHAR(32),
	[AppId]				NVARCHAR(32),
	[OpenId]			NVARCHAR(32),
	[RegistrySource]	INT,
	[NickName]			NVARCHAR(50),
	[Country]			NVARCHAR(50),
	[Province]			NVARCHAR(50),
	[City]				NVARCHAR(50),	
	[AvatarUrl]			NVARCHAR(200),
	[LastUpdatedBy]		NVARCHAR(50),
	[SharedBy]			NVARCHAR(32),
	[ScenarioId]		UNIQUEIDENTIFIER
)
