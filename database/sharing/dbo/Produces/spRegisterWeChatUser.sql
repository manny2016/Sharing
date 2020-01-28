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
	ON [target].[AppId]= [source].[AppId] AND [target].[OpenId] = [source].[OpenId]
	WHEN MATCHED THEN UPDATE SET 	
		[target].[LastUpdatedBy] = [source].[LastUpdatedBy],
		[target].[LastUpdatedDateTime] = DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([UnionId],[RegistrySource],[NickName],
		[Country],[Province],[City],[AvatarUrl],[AppId],[OpenId],
		[CreatedDateTime],[CreatedBy],[ScenarioId])
	  VALUES([source].[UnionId],[source].[RegistrySource],[source].[NickName],
	  [source].[Country],[source].[Province],[source].[City],[source].[AvatarUrl],
	  [source].[AppId],[source].[OpenId],DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),[source].[LastUpdatedBy],[source].[ScenarioId]);
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
