#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab2.Web/Lab2.Web.csproj", "Lab2.Web/"]
RUN dotnet restore "Lab2.Web/Lab2.Web.csproj"
COPY . .
WORKDIR "/src/Lab2.Web"
RUN dotnet build "Lab2.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab2.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab2.Web.dll"]