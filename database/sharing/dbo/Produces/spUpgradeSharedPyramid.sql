CREATE PROCEDURE [dbo].[spUpgradeSharedPyramid]
	@sharedByOpenId		NVARCHAR(32),	
	@currentOpenId		NVARCHAR(32),
	@appid				NVARCHAR(32)
AS
	IF NOT EXISTS (
		SELECT [WxUserId] FROM [dbo].[SharedPyramid] (NOLOCK) [shared]
			LEFT JOIN [dbo].[WxUser] (NOLOCK) [user]
			ON [shared].WxUserId =[user].Id
			WHERE [user].[AppId] = @appid AND [user].[OpenId] = @currentOpenId 
	) 
	BEGIN
		INSERT INTO [dbo].[SharedPyramid]([WxUserId],[MerchantId],[InvitedBy])
		SELECT [user].[Id] AS WxUserId, 
		(SELECT TOP 1 [MerchantId] FROM [dbo].[MWeChatApp] WHERE AppId = @appid) AS [MerchantId],
		(SELECT TOP 1 Id FROM [dbo].[WxUser] (NOLOCK) WHERE OpenId = @sharedByOpenId AND AppId = @appid ) AS [InvitedBy]
		FROM [dbo].[WxUser] (NOLOCK) [user]
		WHERE [AppId] = @appid AND 	[OpenId] = @currentOpenId
	END
RETURN 0
