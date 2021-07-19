#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/eShopEnterprise.Pedido.API/eShopEnterprise.Pedido.API.csproj", "src/services/eShopEnterprise.Pedido.API/"]
RUN dotnet restore "src/services/eShopEnterprise.Pedido.API/eShopEnterprise.Pedido.API.csproj"
COPY . .
WORKDIR "/src/src/services/eShopEnterprise.Pedido.API"
RUN dotnet build "eShopEnterprise.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopEnterprise.Pedido.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopEnterprise.Pedido.API.dll"]