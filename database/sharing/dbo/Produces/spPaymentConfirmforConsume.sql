CREATE PROCEDURE [dbo].[spPaymentConfirmforConsume]
	@id BIGINT,
	@mchid BIGINT,
	@wxUserId BIGINT,
	@rewardTo BIGINT,
	@rewardMoney INT,
	@rewardIntegral INT,
	@confirmTime BIGINT,
	@state INT
AS
BEGIN TRY
	BEGIN TRANSACTION	
	--修改订单支付状态
		DECLARE @details NVARCHAR(MAX);
		SET @details = (SELECT  [Attach] FROM [dbo].[Trade] WHERE [Id]=@id);
		UPDATE [dbo].[Trade] SET 
			TradeState = TradeState ^ @state,
			ConfirmTime =@confirmTime,
			LastUpdatedBy = 'API',
			LastUpdatedDateTime = DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())
		WHERE Id = @id;
		MERGE INTO [dbo].[Product] AS [target]
		USING(
			SELECT [Id],[Number] FROM OPENJSON(
			(SELECT  [Value] FROM OPENJSON(@details,'$.details')))
			WITH(
				[Id] BIGINT '$.id',
				[Number] INT '$.number'
			)
		) AS  [source]([Id],[Number])
		ON [target].[Id] = [source].[Id]
		WHEN MATCHED THEN UPDATE SET [target].[SalesVol] = [target].[SalesVol] + [source].[Number];

	--奖励积分
	IF(@state = 8)
	BEGIN
		INSERT INTO [dbo].[RewardLogging]
		([MerchantId],[WxUserId],[RelevantTradeId],[RewardIntegral],[State],[CreatedBy],[CreatedDateTime])
		VALUES(@mchid,@wxUserId,@id,@rewardIntegral,1,'API',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()));
	END
	
	IF (@rewardTo <> -1 AND @state = 8 )
	BEGIN
		--派发鼓励奖
		INSERT INTO [dbo].[RewardLogging]
		([MerchantId],[WxUserId],[RelevantTradeId],[RewardMoney],[State],[CreatedBy],[CreatedDateTime])
		VALUES(@mchid,@rewardTo,@id,@rewardMoney,1,'API',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()));
	END; 
	
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
