# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

# Use the .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/build

# Copy only the project file
COPY ["PortfolioBackend/PortfolioBackend.csproj", "PortfolioBackend/"]
WORKDIR "/app/build/PortfolioBackend"

# Restore dependencies
RUN dotnet restore

# Ensure bin/ and obj/ exist BEFORE setting permissions
RUN mkdir -p /app/build/bin /app/build/obj
RUN chmod -R 777 /app/build/bin /app/build/obj

# FIX: Remove & recreate apphost file to prevent permission issues
RUN find /app/build/PortfolioBackend/obj -name "apphost" -delete

# Build the project
RUN dotnet publish PortfolioBackend.csproj -c Release --no-self-contained --output /app/out

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PortfolioBackend.dll"]
 
