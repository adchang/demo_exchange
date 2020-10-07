DROP TABLE IF EXISTS dbo.ExchangeTransaction ;
GO

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

DROP PROCEDURE IF EXISTS dbo.ExchangeTransaction_Insert ;
GO

CREATE PROCEDURE dbo.ExchangeTransaction_Insert
  @TransactionId uniqueidentifier,
  @CreatedTimestamp bigint,
  @BuyOrderOrderId uniqueidentifier,
  @SellOrderOrderId uniqueidentifier,
  @Ticker varchar(25),
  @Quantity int,
  @Price decimal(28, 8)
AS
BEGIN
  INSERT INTO dbo.ExchangeTransaction
    (
    TransactionId,
    CreatedTimestamp,
    BuyOrderOrderId,
    SellOrderOrderId,
    Ticker,
    Quantity,
    Price
    )
  VALUES
    (
      @TransactionId,
      @CreatedTimestamp,
      @BuyOrderOrderId,
      @SellOrderOrderId,
      @Ticker,
      @Quantity,
      @Price
    )
END ;
GO

GRANT EXEC ON dbo.ExchangeTransaction_Insert TO demo_exchange_app ;
GO
