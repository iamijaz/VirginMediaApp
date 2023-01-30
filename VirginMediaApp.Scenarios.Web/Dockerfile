#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VirginMediaApp.Scenarios.Web/VirginMediaApp.Scenarios.Web.csproj", "VirginMediaApp.Scenarios.Web/"]
COPY ["VirginMediaApp.Scenarios.Core/VirginMediaApp.Scenarios.Core.csproj", "VirginMediaApp.Scenarios.Core/"]
RUN dotnet restore "VirginMediaApp.Scenarios.Web/VirginMediaApp.Scenarios.Web.csproj"
COPY . .
WORKDIR "/src/VirginMediaApp.Scenarios.Web"
RUN dotnet build "VirginMediaApp.Scenarios.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VirginMediaApp.Scenarios.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirginMediaApp.Scenarios.Web.dll"]