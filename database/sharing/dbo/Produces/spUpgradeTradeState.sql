CREATE PROCEDURE [dbo].[spUpgradeTradeState]
	@tradeId NVARCHAR(50),
	@tradeState INT,
	@o_state INT OUTPUT
AS
	UPDATE [dbo].[Trade] SET [TradeState] = [TradeState] ^ @tradeState WHERE [TradeId]= @tradeId;
	SET @o_state = (SELECT TOP 1 [TradeState] FROM [dbo].[Trade] WHERE [TradeId]=@tradeId);
RETURN 0
