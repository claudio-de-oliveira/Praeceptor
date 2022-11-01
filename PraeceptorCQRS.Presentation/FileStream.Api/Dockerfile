#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FileStream.Api/FileStream.Api.csproj", "FileStream.Api/"]
RUN dotnet restore "FileStream.Api/FileStream.Api.csproj"
COPY . .
WORKDIR "/src/FileStream.Api"
RUN dotnet build "FileStream.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileStream.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileStream.Api.dll"]