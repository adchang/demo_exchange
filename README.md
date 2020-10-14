# Demo Exchange

POC for an exchange written in C# and backed by SQLServer

## Features

- Currently supports Market and Limit orders
- Price-time priority
    - Buy limit price protection
- Quote, Level 2

## [Roadmap](https://docs.google.com/document/d/1cFWrQyfAGYdtYoevVmW26hGT1Nw2f16iywPYAVQoVTM/edit)
- Stop/Trailing Stop
- Order expiration
- Order Update/Cancel

## Solution Details

### Docker images

- mcr.microsoft.com/dotnet/sdk:5.0
- mcr.microsoft.com/dotnet/aspnet:5.0
- mcr.microsoft.com/mssql/server:2019-latest
- redis:latest
- mcr.microsoft.com/azure-cli:latest

### Aliases

Handy aliases for your environment

    source .bash_aliases_demo_exchange
