FROM mcr.microsoft.com/dotnet/aspnet:8.0.6 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0.302 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PersonClientService.WebAPI/PersonClientService.WebAPI.csproj", "PersonClientService.WebAPI/"]
COPY ["PersonClientService.Application/PersonClientService.Application.csproj", "PersonClientService.Application/"]
COPY ["PersonClientService.Infrastructure/PersonClientService.Infrastructure.csproj", "PersonClientService.Infrastructure/"]
COPY ["PersonClientService.Core/PersonClientService.Core.csproj", "PersonClientService.Core/"]

RUN dotnet restore "PersonClientService.WebAPI/PersonClientService.WebAPI.csproj"

COPY . .

WORKDIR "/src/PersonClientService.WebAPI"
RUN dotnet build "PersonClientService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PersonClientService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonClientService.WebAPI.dll"]