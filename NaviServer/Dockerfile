#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal-arm64v8 AS build
WORKDIR /src
COPY ["NaviServer/NaviServer.csproj", "NaviServer/"]
RUN dotnet restore "NaviServer/NaviServer.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/NaviServer"
RUN dotnet build "NaviServer.csproj" -c Release -o /app/build -r linux-arm64

FROM build AS publish
RUN dotnet publish "NaviServer.csproj" -c Release -o /app/publish -r linux-arm64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NaviServer.dll"]