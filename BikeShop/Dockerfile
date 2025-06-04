FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . . 
RUN dotnet restore "./BikeShop/BikeShop.csproj" --disable-parallel
RUN dotnet publish "./BikeShop/BikeShop.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "BikeShop.dll"]