# Database

## Docker SQLServer 2019

https://hub.docker.com/_/microsoft-mssql-server

    1. docker pull mcr.microsoft.com/mssql/server:2019-latest
    2. docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
    3. run _createDatabase.sql scripts as sa
    4. run 000_buildDatabase.sql as admin login

Shrink log file

    DBCC SHRINKFILE (demo_exchange_log, 1)
