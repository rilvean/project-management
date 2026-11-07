FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=5000
EXPOSE 5000
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/ProjectManagement.Api/ProjectManagement.Api.csproj", "src/ProjectManagement.Api/"]
COPY ["src/ProjectManagement.Domain/ProjectManagement.Domain.csproj", "src/ProjectManagement.Domain/"]
COPY ["src/ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj", "src/ProjectManagement.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "src/ProjectManagement.Api/ProjectManagement.Api.csproj"

COPY . .
WORKDIR "/src/src/ProjectManagement.Api"
RUN dotnet publish "ProjectManagement.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement.Api.dll"]
