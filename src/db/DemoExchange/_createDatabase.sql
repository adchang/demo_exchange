-- run as sa

USE master ;
GO

DROP DATABASE IF EXISTS demo_exchange ;
GO

CREATE DATABASE demo_exchange
ON   
(   
  NAME = demo_exchange, 
  FILENAME = '/var/opt/mssql/demo_exchange/demo_exchange.mdf', 
  SIZE = 10MB, 
  MAXSIZE = 50MB,  
  FILEGROWTH = 5MB
) 
LOG ON 
(
  NAME = demo_exchange_log, 
  FILENAME = '/var/opt/mssql/demo_exchange/demo_exchange.ldf', 
  SIZE = 5MB,  
  MAXSIZE = 25MB,  
  FILEGROWTH = 5MB 
) 
COLLATE Latin1_General_100_CI_AI_SC_UTF8 ;
GO

ALTER DATABASE demo_exchange SET RECOVERY SIMPLE ;
GO

-- DBCC SHRINKFILE (demo_exchange_log, 1)

DROP LOGIN demo_exchange_admin ;
GO

CREATE LOGIN demo_exchange_admin WITH PASSWORD = 'PASSWORD' ;
GO

USE demo_exchange
GO

IF NOT EXISTS 
(
SELECT *
FROM sys.database_principals
WHERE name = N'demo_exchange_admin'
)
BEGIN
  CREATE USER [demo_exchange_admin] FOR LOGIN [demo_exchange_admin]
  EXEC sp_addrolemember 'db_owner', 'demo_exchange_admin'
END;
GO

USE master ;
GO

DROP LOGIN demo_exchange_user ;
GO

CREATE LOGIN demo_exchange_user WITH PASSWORD = 'PASSWORD' ;
GO

USE demo_exchange
GO

IF NOT EXISTS 
(
SELECT *
FROM sys.database_principals
WHERE name = N'demo_exchange_user'
)
BEGIN
  CREATE USER [demo_exchange_user] FOR LOGIN [demo_exchange_user]
END;
GO

DROP ROLE IF EXISTS demo_exchange_app ;
GO

CREATE ROLE demo_exchange_app AUTHORIZATION demo_exchange_admin ;
GO

ALTER ROLE demo_exchange_app ADD MEMBER demo_exchange_user ;
GO
