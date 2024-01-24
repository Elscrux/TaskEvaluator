FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskEvaluator.Dataset/TaskEvaluator.Dataset.csproj", "TaskEvaluator.Dataset/"]
RUN dotnet restore "TaskEvaluator.Dataset/TaskEvaluator.Dataset.csproj"
COPY . .
WORKDIR "/src/TaskEvaluator.Dataset"
RUN dotnet build "TaskEvaluator.Dataset.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskEvaluator.Dataset.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEvaluator.Dataset.dll"]
