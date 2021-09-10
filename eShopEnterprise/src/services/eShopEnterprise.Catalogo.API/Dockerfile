#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./services/eShopEnterprise.Catalogo.API/eShopEnterprise.Catalogo.API.csproj", "services/eShopEnterprise.Catalogo.API/"]
COPY ["./building blocks/eShopEnterprise.Core/eShopEnterprise.Core.csproj", "building blocks/eShopEnterprise.Core/"]
COPY ["./building blocks/eShopEnterprise.MessageBus/eShopEnterprise.MessageBus.csproj", "building blocks/eShopEnterprise.MessageBus/"]
COPY ["./building blocks/eShopEnterprise.WebApi.Core/eShopEnterprise.WebApi.Core.csproj", "building blocks/eShopEnterprise.WebApi.Core/"]

RUN dotnet restore "services/eShopEnterprise.Catalogo.API/eShopEnterprise.Catalogo.API.csproj"
COPY . .
WORKDIR "services/eShopEnterprise.Catalogo.API"
RUN dotnet build "eShopEnterprise.Catalogo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopEnterprise.Catalogo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopEnterprise.Catalogo.API.dll"]