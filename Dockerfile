FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:7506
EXPOSE 7506



# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["src/OpenTelemetry.ElasticSearch.Logs/OpenTelemetry.ElasticSearch.Logs.csproj", "src/OpenTelemetry.ElasticSearch.Logs/"]
RUN dotnet restore "src/OpenTelemetry.ElasticSearch.Logs/OpenTelemetry.ElasticSearch.Logs.csproj"
COPY . .
WORKDIR "/src/src/OpenTelemetry.ElasticSearch.Logs"
RUN dotnet build "OpenTelemetry.ElasticSearch.Logs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenTelemetry.ElasticSearch.Logs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenTelemetry.ElasticSearch.Logs.dll"]
