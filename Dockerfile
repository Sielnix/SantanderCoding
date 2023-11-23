#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Santander.Coding.sln", "Santander.Coding.sln"]
COPY ["Santander.Coding/Santander.Coding.csproj", "Santander.Coding/"]
COPY ["Santander.Coding.Tests/Santander.Coding.Tests.csproj", "Santander.Coding.Tests/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/Santander.Coding"
RUN dotnet build --no-restore -c $BUILD_CONFIGURATION
RUN dotnet test --no-restore -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Santander.Coding.csproj" -c $BUILD_CONFIGURATION --no-build -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Santander.Coding.dll"]
