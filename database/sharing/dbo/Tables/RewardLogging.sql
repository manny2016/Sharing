CREATE TABLE [dbo].[RewardLogging]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_RewardLoggingId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_RewardLogging_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[WxUserId] BIGINT NOT NULL CONSTRAINT [FK_RewardLogging_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),
	[RelevantTradeId] BIGINT CONSTRAINT [FK_RewardLogging_TradeId] FOREIGN KEY REFERENCES [dbo].[Trade] ([Id]) NULL,	
	[RewardMoney] INT,	
	[RewardIntegral] INT,
	[State] INT,
	[Description] NVARCHAR(100),
	[CreatedBy] NVARCHAR(50) NULL, 
	[CreatedDateTime] BIGINT NULL,         
	[LastUpdatedBy] NVARCHAR(50) NULL,
	[LastUpdatedDateTime] BIGINT NULL, 
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
