CREATE TABLE dbo.ExchangeAccount
(
  AccountId uniqueidentifier PRIMARY KEY,
  CreatedTimestamp bigint NOT NULL,
  LastUpdatedTimestamp bigint NOT NULL,
  Status varchar(50) NOT NULL,
  FirstName nvarchar(50) NOT NULL,
  MiddleName nvarchar(50),
  LastName nvarchar(50) NOT NULL
) ;
GO

GRANT SELECT, INSERT, UPDATE ON dbo.ExchangeAccount TO demo_exchange_app ;
GO

CREATE TABLE dbo.ExchangeAccountAddress
(
  AddressId uniqueidentifier PRIMARY KEY,
  AccountId uniqueidentifier,
  CreatedTimestamp bigint NOT NULL,
  LastUpdatedTimestamp bigint NOT NULL,
  Type varchar(25) NOT NULL,
  Line1 nvarchar(255) NOT NULL,
  Line2 nvarchar(255),
  Subdistrict nvarchar(50),
  District nvarchar(50),
  City nvarchar(50) NOT NULL,
  Province nvarchar(50) NOT NULL,
  PostalCode nvarchar(25) NOT NULL,
  Country nvarchar(50) NOT NULL,
  CONSTRAINT FK_ExchangeAccountAddress_AccountId FOREIGN KEY (AccountId) REFERENCES dbo.ExchangeAccount (AccountId)
) ;
GO

GRANT SELECT, INSERT, UPDATE ON dbo.ExchangeAccountAddress TO demo_exchange_app ;
GO
