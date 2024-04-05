#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MainService/MainService.API/MainService.API.csproj", "MainService/MainService.API/"]
COPY ["MainService/Main.Domain/Main.Domain.csproj", "MainService/Main.Domain/"]
COPY ["MainService/Main.Infrastructure/Main.Infrastructure.csproj", "MainService/Main.Infrastructure/"]
COPY ["Common/CommonLib/CommonLib.csproj", "Common/CommonLib/"]
RUN dotnet restore "MainService/MainService.API/MainService.API.csproj"
COPY . .
WORKDIR "/src/MainService/MainService.API"
RUN dotnet build "MainService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MainService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainService.API.dll"]