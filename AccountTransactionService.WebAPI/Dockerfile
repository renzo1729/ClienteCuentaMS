FROM mcr.microsoft.com/dotnet/aspnet:8.0.6 AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0.302 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia los archivos .csproj de todos los proyectos
COPY ["AccountTransactionService.WebAPI/AccountTransactionService.WebAPI.csproj", "AccountTransactionService.WebAPI/"]
COPY ["AccountTransactionService.Application/AccountTransactionService.Application.csproj", "AccountTransactionService.Application/"]
COPY ["AccountTransactionService.Infrastructure/AccountTransactionService.Infrastructure.csproj", "AccountTransactionService.Infrastructure/"]
COPY ["AccountTransactionService.Core/AccountTransactionService.Core.csproj", "AccountTransactionService.Core/"]

# Restaura las dependencias
RUN dotnet restore "AccountTransactionService.WebAPI/AccountTransactionService.WebAPI.csproj"

# Copia el resto del código fuente
COPY . .

WORKDIR "/src/AccountTransactionService.WebAPI"
RUN dotnet build "AccountTransactionService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AccountTransactionService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountTransactionService.WebAPI.dll"]
