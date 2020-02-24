CREATE TABLE [dbo].[WxUserIdentity]
(
	[WxUserId]		BIGINT FOREIGN KEY REFERENCES [dbo].[WxUser]([Id]),		
	[AppId]			NVARCHAR(32)	NOT NULL,
	[OpenId]		NVARCHAR(32)	NOT NULL
)	
GO
CREATE UNIQUE INDEX [IDX_AppId_OpenId] ON [dbo].[WxUserIdentity]([AppId],[OpenId])