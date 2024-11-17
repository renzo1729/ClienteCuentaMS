FROM mcr.microsoft.com/dotnet/sdk:8.0.302 AS build
WORKDIR /app

# Copiar archivos de proyecto y restaurar dependencias
COPY . .
RUN dotnet restore
RUN dotnet build --configuration Release

# Ejecutar los tests de integración
CMD dotnet test AccountTransactionService.IntegrationTests/AccountTransactionService.IntegrationTests.csproj --configuration Release --logger trx
