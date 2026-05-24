# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["GNEZDO.csproj", "./"] 
RUN dotnet restore "GNEZDO.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "GNEZDO.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "GNEZDO.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Install wget for healthcheck (lighter than curl)
RUN apt-get update && apt-get install -y wget && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD wget -q --spider http://localhost:8080/ || exit 1

ENTRYPOINT ["dotnet", "GNEZDO.dll"]