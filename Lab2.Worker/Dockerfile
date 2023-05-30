#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab2.Worker/Lab2.Worker.csproj", "Lab2.Worker/"]
RUN dotnet restore "Lab2.Worker/Lab2.Worker.csproj"
COPY . .
WORKDIR "/src/Lab2.Worker"
RUN dotnet build "Lab2.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab2.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab2.Worker.dll"]