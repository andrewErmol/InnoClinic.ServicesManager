#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServicesManager.API/ServicesManager.API/ServicesManager.API.csproj", "ServicesManager.API/ServicesManager.API/"]
COPY ["ServicesManager.API/ServicesManager.Persistence/ServicesManager.Persistence.csproj", "ServicesManager.API/ServicesManager.Persistence/"]
COPY ["ServicesManager.API/ServicesManager.Domain/ServicesManager.Domain.csproj", "ServicesManager.API/ServicesManager.Domain/"]
COPY ["ServicesManager.API/ServicesManager.Presentation/ServicesManager.Presentation.csproj", "ServicesManager.API/ServicesManager.Presentation/"]
COPY ["ServicesManager.API/ModelsToRequest/ServicesManager.ModelsToRequest.csproj", "ServicesManager.API/ModelsToRequest/"]
COPY ["ServicesManager.API/ServicesManager.Services.Abstractions/ServicesManager.Services.Abstractions.csproj", "ServicesManager.API/ServicesManager.Services.Abstractions/"]
COPY ["ServicesManager.API/ServicesManager.Contracts/ServicesManager.Contracts.csproj", "ServicesManager.API/ServicesManager.Contracts/"]
COPY ["ServicesManager.API/ServiceManager.Services/ServicesManager.Services.csproj", "ServicesManager.API/ServiceManager.Services/"]
RUN dotnet restore "ServicesManager.API/ServicesManager.API/ServicesManager.API.csproj"
COPY . .
WORKDIR "/src/ServicesManager.API/ServicesManager.API"
RUN dotnet build "ServicesManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesManager.API.dll"]