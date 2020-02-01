﻿CREATE TABLE [dbo].[RewardLogging]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_RewardLoggingId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_RewardLogging_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[WxUserId] BIGINT NOT NULL CONSTRAINT [FK_RewardLogging_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),
	[RelevantTradeId] BIGINT CONSTRAINT [FK_RewardLogging_TradeId] FOREIGN KEY REFERENCES [dbo].[Trade] ([Id]),	
	[RewardMoney] INT,	
	[RewardIntegral] INT,
	[State] INT,
	[CreatedBy] NVARCHAR(50) NULL, 
	[CreatedDateTime] BIGINT NULL,         
	[LastUpdatedBy] NVARCHAR(50) NULL,
	[LastUpdatedDateTime] BIGINT NULL, 
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
