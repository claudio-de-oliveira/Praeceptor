#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Administrative.App/Administrative.App.csproj", "Administrative.App/"]
RUN dotnet restore "Administrative.App/Administrative.App.csproj"
COPY . .
WORKDIR "/src/Administrative.App"
RUN dotnet build "Administrative.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Administrative.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Administrative.App.dll"]


#FROM node:alpine
#COPY . /app
#CMD node /app/app.js

#FROM node:alpine
#COPY . /app
#WORKDIR /app
#CMD node app.js
