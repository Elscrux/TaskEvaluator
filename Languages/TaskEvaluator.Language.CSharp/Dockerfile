FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Languages/TaskEvaluator.Language.CSharp/TaskEvaluator.Language.CSharp.csproj", "Languages/TaskEvaluator.Language.CSharp/"]
COPY ["TaskEvaluator/TaskEvaluator.csproj", "TaskEvaluator/"]
COPY ["TaskEvaluator.SonarQube/TaskEvaluator.SonarQube.csproj", "TaskEvaluator.SonarQube/"]
RUN dotnet restore "Languages/TaskEvaluator.Language.CSharp/TaskEvaluator.Language.CSharp.csproj"
COPY . .
WORKDIR "/src/Languages/TaskEvaluator.Language.CSharp"
RUN dotnet build "TaskEvaluator.Language.CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskEvaluator.Language.CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEvaluator.Language.CSharp.dll"]
