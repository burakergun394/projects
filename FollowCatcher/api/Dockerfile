FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["src/FollowCatcher.Api/FollowCatcher.Api.csproj", "src/FollowCatcher.Api/"]
COPY ["src/FollowCatcher.Application/FollowCatcher.Application.csproj", "src/FollowCatcher.Application/"]
COPY ["src/FollowCatcher.Domain/FollowCatcher.Domain.csproj", "src/FollowCatcher.Domain/"]
COPY ["src/FollowCatcher.Infrastructure/FollowCatcher.Infrastructure.csproj", "src/FollowCatcher.Infrastructure/"]
COPY ["src/FollowCatcher.Persistence/FollowCatcher.Persistence.csproj", "src/FollowCatcher.Persistence/"]
RUN dotnet restore "src/FollowCatcher.Api/FollowCatcher.Api.csproj"
COPY . .
WORKDIR "/src/src/FollowCatcher.Api"
RUN dotnet build "FollowCatcher.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FollowCatcher.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FollowCatcher.Api.dll"]
