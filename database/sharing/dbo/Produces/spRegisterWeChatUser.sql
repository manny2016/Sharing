CREATE PROCEDURE [dbo].[spRegisterWeChatUser]
	@wxuser  [dbo].[RegisterWeChatUserStructure] READONLY
AS	
BEGIN TRY
	BEGIN TRANSACTION
	MERGE INTO [dbo].[WxUser] AS [target]
	USING(
		SELECT [UnionId],[AppId],[OpenId],[RegistrySource],[NickName],
		[Country],[Province],[City],[AvatarUrl],[LastUpdatedBy],[ScenarioId] FROM @wxuser
	)
	AS [source]
	ON [target].[UnionId]= [source].[UnionId] 
	WHEN MATCHED THEN UPDATE  SET 	
		[target].[LastUpdatedBy] = [source].[LastUpdatedBy],
		[target].[LastUpdatedDateTime] = DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([UnionId],[RegistrySource],[NickName],
		[Country],[Province],[City],[AvatarUrl],
		[CreatedDateTime],[CreatedBy],[ScenarioId])
	  VALUES([source].[UnionId],[source].[RegistrySource],[source].[NickName],
	  [source].[Country],[source].[Province],[source].[City],[source].[AvatarUrl],
	  DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),[source].[LastUpdatedBy],[source].[ScenarioId]);
	
	MERGE INTO [dbo].[WxUserIdentity] AS [target]
	USING(
		SELECT [wuser].[Id] AS [WxUserId],[user].[AppId],[user].[OpenId],[wuser].[UnionId] FROM @wxuser [user]
		LEFT JOIN [dbo].[WxUser] (NOLOCK) [wuser] 
			ON  [user].[UnionId] = [wuser].[UnionId]
	) AS [source]([WxUserId],[AppId],[OpenId],[UnionId])
	ON [target].[AppId]=[source].[AppID] AND [target].[OpenId] =[source].[OpenId]
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([WxUserId],[AppId],[OpenId])
		VALUES([source].[WxUserId],[source].[AppId],[source].[OpenId]);

	MERGE INTO [dbo].[SharedPyramid] AS [target]
	USING(
		SELECT [WxUserId],[MerchantId],[InvitedBy] FROM (
			SELECT [user].[WxUserId],
				[app].[MerchantId],
				(SELECT [WxUserId] FROM  [dbo].[WxUserIdentity] (NOLOCK) [t] WHERE [t].[AppId] = [nuser].[AppId] AND [t].[OpenId] = [nuser].[SharedBy]) AS [InvitedBy]
			FROM @wxuser [nuser]
			LEFT JOIN [dbo].[WxUserIdentity] (NOLOCK) [user]		
				ON [nuser].[AppId] = [user].[AppId] AND [nuser].[OpenId] = [user].[OpenId]
			LEFT JOIN [dbo].[MWeChatApp] (NOLOCK) [app]
				ON [app].[AppId] = [nuser].[AppId]
		) [src] WHERE [src].[InvitedBy] IS NOT NULL
	) AS [source]([WxUserId],[MerchantId],[InvitedBy])
	ON [target].[WxUserId]= [source].[WxUserId] AND [target].[MerchantId] = [source].[MerchantId]
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT([WxUserId],[MerchantId],[InvitedBy])
		VALUES([source].[WxUserId],[source].[MerchantId],[source].[InvitedBy]);		
	
	 COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
RETURN 0
