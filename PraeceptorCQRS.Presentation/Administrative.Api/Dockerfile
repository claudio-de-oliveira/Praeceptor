#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["./Administrative.Api.csproj", "Administrative/"]

#COPY ["../../PraeceptorCQRS.Application/PraeceptorCQRS.Application.csproj", "Application/"]
COPY ["../../PraeceptorCQRS.Application/*.Application.csproj", "Application/"]
#
## #RUN dotnet restore "Application/PraeceptorCQRS.Application.csproj"
#
#COPY ["./../../PraeceptorCQRS.Contracts/*.csproj", "Contracts/"]
#
## #RUN dotnet restore "Contracts/PraeceptorCQRS.Contracts.csproj"
#
#COPY ["./../../PraeceptorCQRS.Infrastructure/*.csproj", "Infrastructure/"]
#
## #RUN dotnet restore "Infrastructure/PraeceptorCQRS.Infrastructure.csproj"
#
#COPY ["./../../PraeceptorCQRS.Utilities/*.csproj", "Utilities/"]
#
## #RUN dotnet restore "Utilities/PraeceptorCQRS.Utilities.csproj"

RUN dotnet restore "Administrative/Administrative.Api.csproj"

COPY . .
WORKDIR "/src/Administrative"
RUN dotnet build "AdministrativeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdministrativeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Administrative.Api.dll"]