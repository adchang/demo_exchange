CREATE TABLE dbo.ExchangeTransaction
(
  TransactionId uniqueidentifier PRIMARY KEY,
  CreatedTimestamp bigint NOT NULL,
  BuyOrderOrderId uniqueidentifier NOT NULL,
  SellOrderOrderId uniqueidentifier NOT NULL,
  Ticker varchar(25) NOT NULL,
  Quantity int NOT NULL,
  Price decimal(28, 8) NOT NULL,
  CONSTRAINT FK_ExchangeTransaction_BuyOrderOrderId FOREIGN KEY (BuyOrderOrderId) REFERENCES dbo.ExchangeOrder (OrderId),
  CONSTRAINT FK_ExchangeTransaction_SellOrderOrderId FOREIGN KEY (SellOrderOrderId) REFERENCES dbo.ExchangeOrder (OrderId)
) ;
GO

GRANT SELECT, INSERT, UPDATE ON dbo.ExchangeTransaction TO demo_exchange_app ;
GO

