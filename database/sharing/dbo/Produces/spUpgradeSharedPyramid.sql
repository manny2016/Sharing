CREATE PROCEDURE [dbo].[spUpgradeSharedPyramid]
	@sharedByOpenId		NVARCHAR(32),	
	@currentOpenId		NVARCHAR(32),
	@appid				NVARCHAR(32)
AS
	IF NOT EXISTS (
		SELECT [shared].[WxUserId] FROM [dbo].[SharedPyramid] (NOLOCK) [shared]
			LEFT JOIN [dbo].[WxUserIdentity] (NOLOCK) [user]
			ON [shared].WxUserId =[user].[WxUserId]
			WHERE [user].[AppId] = @appid AND [user].[OpenId] = @currentOpenId 
	) 
	BEGIN
		INSERT INTO [dbo].[SharedPyramid]([WxUserId],[MerchantId],[InvitedBy])
		SELECT [user].[WxUserId], 
		(SELECT TOP 1 [MerchantId] FROM [dbo].[MWeChatApp] WHERE AppId = @appid) AS [MerchantId],
		(SELECT TOP 1 [WxUserId] FROM [dbo].[WxUserIdentity] (NOLOCK) WHERE OpenId = @sharedByOpenId AND AppId = @appid ) AS [InvitedBy]
		FROM [dbo].[WxUserIdentity] (NOLOCK) [user]
		WHERE [AppId] = @appid AND 	[OpenId] = @currentOpenId
	END
RETURN 0
