FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ks.webapi/ks.webapi.csproj", "src/ks.webapi/"]
COPY ["src/ks.domain/ks.domain.csproj", "src/ks.domain/"]
COPY ["src/ks.application/ks.application.csproj", "src/ks.application/"]
COPY ["src/ks.infras/ks.infras.csproj", "src/ks.infras/"]
FROM build AS publish
RUN dotnet publish "./ks.webapi.csproj" -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ks.webapi.dll"]