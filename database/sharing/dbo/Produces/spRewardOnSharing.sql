CREATE PROCEDURE [dbo].[spRewardOnSharing]
	@appid NVARCHAR(32),  --appid
	@openid NVARCHAR(32),
	@rewardMoneyLimit FLOAT -- 奖励额度,	
AS
	DECLARE @wxUserId BIGINT;
	DECLARE @shared INT;
	SELECT @wxUserId = [WxUserId] FROM [dbo].[WxUserIdentity] (NOLOCK) WHERE [AppId] =@appid AND [OpenId] = @openid;
	SELECT @shared=Shared FROM [dbo].[WxUser] WHERE [Id] = @wxUserId;

	IF(SELECT COUNT([WxUserId]) FROM [dbo].[RewardLogging] (NOLOCK) WHERE [WxUserId] = @wxUserId AND [RewardMoney] IS NOT NULL) =0 AND @shared  = 1
	BEGIN		
		DECLARE @realMoney INT;
		DECLARE @mchid BIGINT;
		DECLARE @relevantTradeId BIGINT
		SELECT @relevantTradeId = [Id],@realMoney = [RealMoney],@mchid = [MerchantId] FROM  [dbo].[Trade] (NOLOCK) 
		WHERE  [WxUserId] =@wxUserId  AND [TradeState] & 8 = 8 ORDER BY [Id] ASC;

		
		IF (@relevantTradeId IS NOT NULL)
		BEGIN
			--派发首次分享现金红包(鼓励金)
			INSERT INTO [dbo].[RewardLogging]
			([MerchantId],[WxUserId],[RelevantTradeId],[RewardMoney],[State],[CreatedBy],[CreatedDateTime],[Description])
			VALUES(@mchid,@wxUserId,@relevantTradeId,@realMoney * @rewardMoneyLimit,1,'API',DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()),'首次分享奖励.');
		END
	END
RETURN 0
