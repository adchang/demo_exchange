FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
ADD src/Utils Utils
ADD src/DemoExchange.Interface DemoExchange.Interface
ADD src/DemoExchange DemoExchange
WORKDIR /src/DemoExchange.OrderService
COPY src/DemoExchange.OrderService/DemoExchange.OrderService.csproj .
RUN dotnet restore
COPY src/DemoExchange.OrderService .
RUN dotnet publish -c release -o /app 
# --no-restore
FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DemoExchange.OrderService.dll"]
