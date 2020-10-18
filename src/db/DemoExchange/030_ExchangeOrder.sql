CREATE TABLE dbo.ExchangeOrder
(
  OrderId uniqueidentifier PRIMARY KEY,
  CreatedTimestamp bigint NOT NULL,
  AccountId uniqueidentifier NOT NULL,
  Status varchar(25) NOT NULL,
  Action varchar(15) NOT NULL,
  Ticker varchar(25) NOT NULL,
  Type varchar(50) NOT NULL,
  Quantity int NOT NULL,
  OpenQuantity int NOT NULL,
  OrderPrice decimal(28, 8) NOT NULL,
  StrikePrice decimal(28, 8) NOT NULL,
  TimeInForce varchar(25) NOT NULL,
  ToBeCanceledTimestamp bigint NOT NULL,
  CanceledTimestamp bigint,
  CONSTRAINT FK_ExchangeOrder_AccountId FOREIGN KEY (AccountId) REFERENCES dbo.ExchangeAccount (AccountId)
) ;
GO

GRANT SELECT, INSERT, UPDATE ON dbo.ExchangeOrder TO demo_exchange_app ;
GO
