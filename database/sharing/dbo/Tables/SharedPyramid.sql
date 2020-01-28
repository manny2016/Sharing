CREATE TABLE [dbo].[SharedPyramid]
(
	[WxUserId]		BIGINT NOT NULL	
		CONSTRAINT [FK_SharedPyramid_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),
	[MerchantId]	BIGINT NOT NULL CONSTRAINT [FK_SharedPyramid_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[InvitedBy]		BIGINT NULL		CONSTRAINT [FK_SharedPyramid_InvitedByWxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),
	CONSTRAINT [PK_SharedPyramid] PRIMARY KEY CLUSTERED 
	 (
		[WxUserId],[MerchantId]
	 )
)
