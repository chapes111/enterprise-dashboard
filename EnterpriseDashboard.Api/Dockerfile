# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EnterpriseDashboard.Api.csproj", "./"]
RUN dotnet restore "EnterpriseDashboard.Api.csproj"
COPY . .
RUN dotnet publish "EnterpriseDashboard.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5025
ENV ASPNETCORE_URLS=http://+:5025
ENTRYPOINT ["dotnet", "EnterpriseDashboard.Api.dll"]
