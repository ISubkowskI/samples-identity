# Identity

A collection of .NET C# projects demonstrating various identity concepts and implementations.

## ae-sample-identity-webapp

This project provides a sample web application demonstrating identity features.

### Prerequisites
Ensure the following tools are installed and configured before proceeding:
- Docker and Docker Desktop
- Kubernetes enabled in Docker Desktop
- `kubectl` command-line tool

### Docker

The project includes a Dockerfile for easy containerization:

ae-sample-identity-webapp.Dockerfile


### Usage

Follow these steps to build and run the application using Docker:

1.  **Build the Docker image:**
    Open your terminal in the same directory as `ae-sample-identity-webapp.Dockerfile` and run:
    ```bash
    docker build -t aesamples/ae-sample-identity-webapp:v1.0.0 -f ae-sample-identity-webapp.Dockerfile .
    ```
2.  **Run the Docker container:**
    * To run interactively (logs shown in the terminal, container removed on stop):
        ```bash
        docker run -it --rm -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp:v1.0.0
        ```
    * To run in detached mode (runs in the background):
        ```bash
        docker run -d -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp:v1.0.0
        ```
3.  **Access the application:**
    Open your web browser and navigate to `http://localhost:8000/apps/sample-identity-webapp`.

4.  **Login Credentials:**
    Use the following credentials to log in:
    - **Email:** `info@softaren.com`
      **Password:** `Demo`
    - **Email:** `notifications@softaren.com`
      **Password:** `Demo`

5.  **Push the Docker image to Docker Hub:**
    * Log in to Docker Hub from the command line:
        ```bash
        docker login -u "<Username>" -p "<Password>" docker.io
        ```
    * Set the Docker image tag (to list images, use the command: docker images):
        ```bash
        docker tag aesamples/ae-sample-identity-webapp:v1.0.0 <dockerhubusername>/ae-sample-identity-webapp:v1.0.0
        ```
    *  Push the Docker image to the Docker Hub repository:
        ```bash
        docker push <dockerhubusername>/ae-sample-identity-webapp:v1.0.0
        ```

### Kubernetes (Docker Desktop)

The solution includes a k8s folder with *.yaml files for deploying the application on Kubernetes:

- ae-sample-identity-webapp-depl.yaml
- ingress-srv.yaml

### Usage

1.  **Deploy application:**
    Open your terminal in the k8s directory and run:
    ```bash
    kubectl apply -f ae-sample-identity-webapp-depl.yaml
    ```
2.  **Modify hosts file:**
    Add the following line to your hosts file (located at `C:\Windows\System32\drivers\etc\hosts` on Windows):
    ```bash
    127.0.0.1 poc.softaren.com
    ```
3.  **Install the ingress-nginx controller on Docker Desktop:**
    * Run the following command:
        ```bash
        kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.1/deploy/static/provider/cloud/deploy.yaml
        ```
    * Configure Ingress-nginx controller:
        ```bash
        kubectl apply -f ingress-srv.yaml
        ```
4.  **Access the application:**
    Open your web browser and navigate to `http://poc.softaren.com/apps/sample-identity-webapp`.