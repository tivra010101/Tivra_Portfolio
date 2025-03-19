# Use official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

# Use the .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore the project
COPY ["PortfolioBackend/PortfolioBackend.csproj", "PortfolioBackend/"]
WORKDIR "/src/PortfolioBackend"
RUN dotnet restore

# Copy the rest of the application
COPY . .
RUN dotnet publish -c Release --no-self-contained -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PortfolioBackend.dll"]
