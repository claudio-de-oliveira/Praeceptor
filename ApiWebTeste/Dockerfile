#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiWebTeste/ApiWebTeste.csproj", "ApiWebTeste/"]
RUN dotnet restore "ApiWebTeste.csproj"
COPY . .
WORKDIR "/src/ApiWebTeste"
RUN dotnet build "ApiWebTeste.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiWebTeste.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiWebTeste.dll"]