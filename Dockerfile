# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and restore dependencies
COPY *.sln .
COPY GoalRoad.Domain/GoalRoad.Domain.csproj GoalRoad.Domain/
COPY GoalRoad.Infrastructure/GoalRoad.Infrastructure.csproj GoalRoad.Infrastructure/
COPY GoalRoad.Application/GoalRoad.Application.csproj GoalRoad.Application/
COPY GoalRoad.IoC/GoalRoad.IoC.csproj GoalRoad.IoC/
COPY GoalRoad.IoC/GoalRoad.Tests.csproj GoalRoad.Tests/
COPY GoalRoad/GoalRoad.csproj GoalRoad/

# Restore NuGet packages
RUN dotnet restore

# Copy all source files
COPY GoalRoad.Domain/ GoalRoad.Domain/
COPY GoalRoad.Infrastructure/ GoalRoad.Infrastructure/
COPY GoalRoad.Application/ GoalRoad.Application/
COPY GoalRoad.IoC/ GoalRoad.IoC/
COPY GoalRoad.Tests/ GoalRoad.Tests/
COPY GoalRoad/ GoalRoad/

# Build the application
WORKDIR /src/GoalRoad
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create directory for ML models
RUN mkdir -p /app/Treinamento

# Copy published files
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Run the application
ENTRYPOINT ["dotnet", "GoalRoad.dll"]

