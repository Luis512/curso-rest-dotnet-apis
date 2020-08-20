#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["eMarketApp/eMarketApp.csproj", "eMarketApp/"]
COPY ["eMarketDomain/eMarketDomain.csproj", "eMarketDomain/"]
RUN dotnet restore "eMarketApp/eMarketApp.csproj"
COPY . .
WORKDIR "/src/eMarketApp"
RUN dotnet build "eMarketApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eMarketApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eMarketApp.dll"]