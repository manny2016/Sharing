﻿CREATE TABLE [dbo].[Trade]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1) CONSTRAINT [PK_TradeId] PRIMARY KEY,
	[MerchantId] BIGINT NOT NULL CONSTRAINT [FK_Trade_MerchantId] FOREIGN KEY REFERENCES [dbo].[Merchant] ([Id]),
	[WxUserId] BIGINT NOT NULL CONSTRAINT [FK_Trade_WxUserId] FOREIGN KEY REFERENCES [dbo].[WxUser] ([Id]),
	[WxOrderId] NVARCHAR(50) INDEX [IDX_Trade_WxOrderId],
	[TradeId] NVARCHAR(50)  CONSTRAINT [UK_TradeId] UNIQUE ([TradeId]),
	[TradeCode] INT,
	[TradeType] INT NOT NULL,
	[TradeState] INT NOT NULL,
	[Money] INT,
	[RealMoney] INT,
	[ConfirmTime] BIGINT NULL,
	[Attach] NVARCHAR(MAX) NULL,
	[Strategy] NVARCHAR(MAX) NULL,
	[CreatedDateTime] BIGINT NULL, 
    [LastUpdatedTime] BIGINT NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [LastUpdatedBy] NVARCHAR(50) NULL              ,
	[ScenarioId] UNIQUEIDENTIFIER NULL
)
