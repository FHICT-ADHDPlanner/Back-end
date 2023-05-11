#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 1001
EXPOSE 1101

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ADHDPlanner-Backend/ADHDPlanner-Backend.csproj", "ADHDPlanner-Backend/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
RUN dotnet restore "ADHDPlanner-Backend/ADHDPlanner-Backend.csproj"
COPY . .
WORKDIR "/src/ADHDPlanner-Backend"
RUN dotnet build "ADHDPlanner-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ADHDPlanner-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ADHDPlanner-Backend.dll"]