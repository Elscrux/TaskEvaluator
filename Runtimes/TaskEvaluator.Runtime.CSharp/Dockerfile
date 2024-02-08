FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Runtimes/TaskEvaluator.Runtime.CSharp/TaskEvaluator.Runtime.CSharp.csproj", "Runtimes/TaskEvaluator.Runtime.CSharp/"]
COPY ["TaskEvaluator/TaskEvaluator.csproj", "TaskEvaluator/"]
COPY ["TaskEvaluator.SonarQube/TaskEvaluator.SonarQube.csproj", "TaskEvaluator.SonarQube/"]
RUN dotnet restore "Runtimes/TaskEvaluator.Runtime.CSharp/TaskEvaluator.Runtime.CSharp.csproj"
COPY . .
WORKDIR "/src/Runtimes/TaskEvaluator.Runtime.CSharp"
RUN dotnet build "TaskEvaluator.Runtime.CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskEvaluator.Runtime.CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskEvaluator.Runtime.CSharp.dll"]
