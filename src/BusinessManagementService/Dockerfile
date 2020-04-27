#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BusinessManagementService/BusinessManagementService.csproj", "BusinessManagementService/"]
RUN dotnet restore "BusinessManagementService/BusinessManagementService.csproj"
COPY . .
WORKDIR "/src/BusinessManagementService"
RUN dotnet build "BusinessManagementService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessManagementService.dll"]