# Copy .csproj an dependent .csproj file(s) to workdir and run restore
# This is the step least expected to change, ie. fewest cache misses
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS restore
WORKDIR /src
COPY AccountService/AccountService.csproj AccountService/
COPY Common/Common.csproj Common/
WORKDIR /src/AccountService
# RUN dotnet restore -r linux-musl-x64
RUN dotnet restore

# Copy the actual project sources to the workdir and run publish to build application
# Any and all changes to either source folder will cause a cache miss
FROM restore AS publish
WORKDIR /src
COPY AccountService/ AccountService/
COPY Common/ Common/
WORKDIR /src/AccountService
# RUN dotnet publish -c Release -o /app -r linux-musl-x64 --no-self-contained --no-restore
RUN dotnet publish -c Release -o /app --no-restore

# Copy output of publish step to workdir, expose ports and tell docker what and how to run it
# If nothing changed in the previous steps, the cache will be used, meaning no building will happen at all
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "AccountService.dll"]