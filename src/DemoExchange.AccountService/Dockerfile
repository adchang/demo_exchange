FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ADD nuget.config .
ADD nuget nuget
ADD src/protos src/protos
WORKDIR /src/DemoExchange.AccountService
COPY src/DemoExchange.AccountService/DemoExchange.AccountService.csproj .
RUN dotnet restore -r linux-x64
COPY src/DemoExchange.AccountService .
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true
FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["./DemoExchange.AccountService"]
