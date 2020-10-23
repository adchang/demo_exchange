FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# Normally, these would be nuget dependencies, but for sake of simplicity, just referencing as project
# Order base on least likely to change to take advantage of Docker cache
ADD src/protos protos
ADD src/DemoExchange.Interface DemoExchange.Interface
WORKDIR /src/DemoExchange.Identity
COPY src/DemoExchange.Identity/DemoExchange.Identity.csproj .
RUN dotnet restore -r linux-x64
COPY src/DemoExchange.Identity .
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true
FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["./DemoExchange.Identity"]
