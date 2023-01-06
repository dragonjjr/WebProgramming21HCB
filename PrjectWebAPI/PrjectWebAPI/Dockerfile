#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PrjectWebAPI/PrjectWebAPI.csproj", "PrjectWebAPI/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "PrjectWebAPI/PrjectWebAPI.csproj"
COPY . .
WORKDIR "/src/PrjectWebAPI"
RUN dotnet build "PrjectWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrjectWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrjectWebAPI.dll"]