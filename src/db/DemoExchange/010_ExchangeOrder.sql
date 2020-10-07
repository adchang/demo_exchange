DROP TABLE IF EXISTS dbo.ExchangeOrder ;
GO

CREATE TABLE dbo.ExchangeOrder
(
  OrderId uniqueidentifier PRIMARY KEY,
  CreatedTimestamp bigint NOT NULL,
  AccountId varchar(25) NOT NULL, -- TODO: Change this to Guid
  Status varchar(25) NOT NULL,
  Action varchar(4) NOT NULL,
  Ticker varchar(25) NOT NULL,
  Type varchar(25) NOT NULL,
  Quantity int NOT NULL,
  OpenQuantity int NOT NULL,
  OrderPrice decimal(28, 8) NOT NULL,
  StrikePrice decimal(28, 8) NOT NULL,
  TimeInForce varchar(25) NOT NULL,
  ToBeCanceledTimestamp bigint NOT NULL,
  CanceledTimestamp bigint
) ;
GO

GRANT SELECT, INSERT, UPDATE ON dbo.ExchangeOrder TO demo_exchange_app ;
GO

DROP PROCEDURE IF EXISTS dbo.ExchangeOrder_Insert ;
GO

CREATE PROCEDURE dbo.ExchangeOrder_Insert
  @OrderId uniqueidentifier,
  @CreatedTimestamp bigint,
  @AccountId uniqueidentifier,
  @Status varchar(25),
  @Action varchar(4),
  @Ticker varchar(25),
  @Type varchar(25),
  @Quantity int,
  @OrderPrice decimal(28, 8),
  @OpenQuantity int,
  @StrikePrice decimal(28, 8),
  @TimeInForce varchar(25),
  @ToBeCanceledTimestamp bigint,
  @CanceledTimestamp bigint
AS
BEGIN
  INSERT INTO dbo.ExchangeOrder
    (
    OrderId,
    CreatedTimestamp,
    AccountId,
    Status,
    Action,
    Ticker,
    Type,
    Quantity,
    OpenQuantity,
    OrderPrice,
    StrikePrice,
    TimeInForce,
    ToBeCanceledTimestamp,
    CanceledTimestamp
    )
  VALUES
    (
      @OrderId,
      @CreatedTimestamp,
      @AccountId,
      @Status,
      @Action,
      @Ticker,
      @Type,
      @Quantity,
      @OpenQuantity,
      @OrderPrice,
      @StrikePrice,
      @TimeInForce,
      @ToBeCanceledTimestamp,
      @CanceledTimestamp
    )
END ;
GO

GRANT EXEC ON dbo.ExchangeOrder_Insert TO demo_exchange_app ;
GO

DROP PROCEDURE dbo.ExchangeOrder_Update ;
GO

CREATE PROCEDURE dbo.ExchangeOrder_Update
  @OrderId uniqueidentifier,
  @Status varchar(25),
  @OpenQuantity int,
  @StrikePrice decimal(28, 8),
  @CanceledTimestamp bigint
AS
BEGIN
  UPDATE dbo.ExchangeOrder 
    SET Status = @Status, 
        OpenQuantity = @OpenQuantity, 
        StrikePrice = @StrikePrice, 
        CanceledTimestamp = @CanceledTimestamp
    WHERE OrderId = @OrderId
END ;
GO

GRANT EXEC ON dbo.ExchangeOrder_Update TO demo_exchange_app ;
GO
