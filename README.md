# Task Evaluator

This tool is built to evaluate the code generated by generative AI tools for coding such as GitHub Copilot.
In total, there are 4 major components to achieve this:

- Code Generator
- Code Evaluator
- Database & Data Visualization
- Interface (App, Web Api or Cli)

## Setup

The tool is using multiple external services that need to be set up to gain full functionality.
There are external tools for code generation, code evaluation and data visualization.

In any case, you'll first have to
- Install Docker
- Start Docker-Engine
- Copy .env.example to .env and fill the commented out variables (see the individual sections below for more information)
- Start the Docker Containers with
    ```bash
    docker-compose up -d
    ```

## Manual Setup

Detailed explanations for the setup of the individual Docker containers.

### GitHub Copilot

- Add the following .NET User Secrets for TaskEvaluator
```json
{
    "GitHubCopilot": {
        "CompletionsUrl": "https://copilot-proxy.githubusercontent.com/v1/engines/copilot-codex/completions",
        "TokenUrl": "https://api.github.com/copilot_internal/v2/token",
        "UserAgent": "GithubCopilot",
        "UserAgentVersion": "1.138.0",
        "EditorVersion": "vscode/1.84.1",
        "EditorPluginVersion": "copilot/1.138.0",
        "BearerToken": "YOUR_BEARER_TOKEN",
        "Openai-Organization": "github-copilot",
        "Openai-Intent": "copilot-ghost"
    }
}
```

- Replace the Bearer Token with your GitHub Copilot Bearer Token
    - Run this [python script](https://github.com/aaamoon/copilot-gpt4-service/blob/master/shells/get_copilot_token.py)
    - Connect with GitHub Account
    - Copy the Bearer Token from the console output

## Tabby
- Pull Tabby Image
    ```bash
    docker pull tabbyml/tabby
    ```
- Add the following .NET User Secrets for TaskEvaluator
```json
{
    "Tabby": {
        "CompletionsUrl": "http://localhost:8080/v1/completions"
    }
}
```
- Start Tabby Container (when running on GPU with CUDA support)
    ```bash
    docker run -it --gpus all -p 8080:8080 -v $HOME/.tabby:/data tabbyml/tabby serve --model TabbyML/StarCoder-1B --device cuda
    ```
- Start Tabby Container (when running on CPU)
    ```bash
    docker run --entrypoint /opt/tabby/bin/tabby-cpu -it -p 8080:8080 -v $HOME/.tabby:/data tabbyml/tabby serve --model TabbyML/StarCoder-1B
    ```

### SonarQube
- Pull SonarQube Image
    ```bash
    docker pull sonarqube
    ```
- Start SonarQube Container
    ```bash
    docker network create taskevaluator_sonarqube_net
    docker run -d --name sonarqube -p 9000:9000 --net taskevaluator_sonarqube_net sonarqube
    ```
    - Open [localhost:9000](http://localhost:9000)
        - Optionally set custom Environment Variable SONARQUBE_URL
    - Add the following .NET User Secrets (use custom credentials if applicable)
```json
{
    "SonarQube": {
        "Url": "http://sonarqube:9000",
        "User": "admin",
        "Password": "admin"
    }
}
```

### Data Sink

#### PostgreSQL Sink
- Pull Postgres Image
    ```bash
    docker pull postgres
    ```
- Start Postgres Container
    ```bash
    docker network create taskevaluator_postgres_net
    docker run -d --name postgres -u postgres -e POSTGRES_PASSWORD=YOUR_PASSWORD -p 5432:5432 --net taskevaluator_postgres_net postgres
    ```
    - Add the following .NET User Secrets

```json
{
    "Database": {
        "ConnectionString": "User ID=postgres;Host=localhost;Port=5432;Password=YOUR_PASSWORD;"
    }
}
```

### Visualization

#### Grafana
- Pull Grafana Image
    ```bash
    docker pull grafana/grafana
    ```
- Start Grafana Container
    ```bash
    docker run -d --name grafana -u grafana -p 3000:3000 --net taskevaluator_postgres_net grafana/grafana
    ```
- Login (default credentials: admin/admin)
- Add a new Data Source
    - Type: PostgreSQL
    - Host: postgres:5432
    - Database: postgres
    - User: postgres
    - Password: YOUR_PASSWORD
    - SSL Mode: disable

## Language Support

Currently, the tool only supports C# as a programming language.

## Running a Task Set

- Create a directory which contains your task set
- Add the path to the directory to your .NET User Secrets
```json
{
    "TaskSet": {
        "DirectoryPath": "YOUR_TASK_SET_DIRECTORY_PATH"
    }
}
```

- The directory should have the following structure:
  - [Language]
    - [TestName]
      - File including the name "Program" for the source code
      - File including the name "UnitTest" for the unit tests
      - metadata.json with additional information
        - Example:
      ```json
      {
          "id": "112c5a6e-0e7c-4e49-b699-8e2be2e24e4a",
          "isHumanEval": true
      }
      ```
- 
- Here an example
    - CSharp
        - Test1
            - Program.cs
            - UnitTests.cs
            - metadata.json
        - Test2
            - Program.cs
            - UnitTest
            - metadata.json
        - Test3
            - MyProgram
            - UnitTests
            // No metadata.json - default values will be used
        - ...
    - ...