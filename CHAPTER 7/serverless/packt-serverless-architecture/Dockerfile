#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["packt-serverless-architecture/packt-serverless-architecture.csproj", "packt-serverless-architecture/"]
RUN dotnet restore "packt-serverless-architecture/packt-serverless-architecture.csproj"
COPY . .
WORKDIR "/src/packt-serverless-architecture"
RUN dotnet build "packt-serverless-architecture.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "packt-serverless-architecture.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "packt-serverless-architecture.dll"]