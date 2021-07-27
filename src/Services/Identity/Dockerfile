#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ./Identity.API/Identity.API.csproj Services/Identity/Identity.API/
COPY ./Identity.Application/Identity.Application.csproj Services/Identity/Identity.Application/
COPY ./Identity.Domain/Identity.Domain.csproj Services/Identity/Identity.Domain/
COPY ./Identity.Infrastructure/Identity.Infrastructure.csproj Services/Identity/Identity.Infrastructure/
COPY ../BuildingBlocks/Common.Logging/Common.Logging.csproj BuildingBlocks/Common.Logging/
RUN dotnet restore "Services/Identity/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]

