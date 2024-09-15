FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Guider.WebApi/Guider.WebApi.csproj", "Guider.WebApi/"]
COPY ["Guider.Application/Guider.Application.csproj", "Guider.Application/"]
COPY ["Guider.Domain/Guider.Domain.csproj", "Guider.Domain/"]
COPY ["Guider.Infrastructure/Guider.Infrastructure.csproj", "Guider.Infrastructure/"]
RUN dotnet restore "Guider.WebApi/Guider.WebApi.csproj"
COPY . .
WORKDIR "/src/Guider.WebApi"
RUN dotnet build "Guider.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Guider.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guider.WebApi.dll"]
