#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PMDB-docker/PMDB-docker.csproj", "PMDB-docker/"]
COPY ["PMDB-docker.Data/PMDB-docker.Data.csproj", "PMDB-docker.Data/"]
COPY ["PMDB-docker.Interfaces/PMDB-docker.Interface.csproj", "PMDB-docker.Interfaces/"]
COPY ["PMDB-docker.Model/PMDB-docker.Models.csproj", "PMDB-docker.Model/"]
COPY ["PMDB-docker.Business/PMDB-docker.Business.csproj", "PMDB-docker.Business/"]
RUN dotnet restore "PMDB-docker/PMDB-docker.csproj"
COPY . .
WORKDIR "/src/PMDB-docker"
RUN dotnet build "PMDB-docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PMDB-docker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMDB-docker.dll"]
