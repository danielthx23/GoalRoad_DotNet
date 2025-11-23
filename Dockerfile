FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY GoalRoad.Domain/GoalRoad.Domain.csproj GoalRoad.Domain/
COPY GoalRoad.Infrastructure/GoalRoad.Infrastructure.csproj GoalRoad.Infrastructure/
COPY GoalRoad.Application/GoalRoad.Application.csproj GoalRoad.Application/
COPY GoalRoad.IoC/GoalRoad.IoC.csproj GoalRoad.IoC/
COPY GoalRoad.Tests/GoalRoad.Tests.csproj GoalRoad.Tests/
COPY GoalRoad/GoalRoad.csproj GoalRoad/

RUN dotnet restore *.sln

COPY GoalRoad.Domain/ GoalRoad.Domain/
COPY GoalRoad.Infrastructure/ GoalRoad.Infrastructure/
COPY GoalRoad.Application/ GoalRoad.Application/
COPY GoalRoad.IoC/ GoalRoad.IoC/
COPY GoalRoad.Tests/ GoalRoad.Tests/
COPY GoalRoad/ GoalRoad/

# Build the application
WORKDIR /src/GoalRoad
RUN dotnet build -c Release -o /app/build

WORKDIR /src/GoalRoad.Tests
RUN dotnet test -c Release --no-restore --verbosity normal

FROM build AS publish
WORKDIR /src/GoalRoad
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

RUN mkdir -p /app/Treinamento

COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "GoalRoad.dll"]
