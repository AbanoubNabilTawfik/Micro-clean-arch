# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/catalog/Catalog.API/Catalog.API.csproj", "services/catalog/Catalog.API/"]
COPY ["services/catalog/Catalog.Application/Catalog.Application.csproj", "services/catalog/Catalog.Application/"]
COPY ["services/catalog/Catalog.Core/Catalog.Core.csproj", "services/catalog/Catalog.Core/"]
COPY ["services/catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "services/catalog/Catalog.Infrastructure/"]
COPY services/catalog/Catalog.Infrastructure/Data/SeedData /src/services/catalog/Catalog.Infrastructure/Data/SeedData
RUN dotnet restore "./services/catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/services/catalog/Catalog.API"
RUN dotnet build "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]