FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BusinessManagementService/BusinessManagementService.csproj", "BusinessManagementService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "BusinessManagementService/BusinessManagementService.csproj"
COPY . .
WORKDIR /src/BusinessManagementService
RUN dotnet build --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessManagementService.dll"]