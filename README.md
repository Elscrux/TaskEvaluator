# Task Evaluator

## Setup
- Install Docker
- Start Docker-Engine
- Start SonarQube
    ```bash
    docker run -d --name sonarqube -p 9000:9000 sonarqube
    ```
    - Open [localhost:9000](http://localhost:9000)
        - Optionally set custom Environment Variable SONARQUBE_URL
    - Login (default credentials: admin/admin)
    - Set a new password
    - Insert credentials into Environment Variables
        - SONARQUBE_USERNAME
        - SONARQUBE_PASSWORD
- Start TaskEvaluator.Api
