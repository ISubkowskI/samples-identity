# Identity

A collection of .NET C# projects demonstrating various identity concepts and implementations.

## ae-sample-identity-webapp

This project provides a sample web application demonstrating identity features.

### Docker

The project includes a Dockerfile for easy containerization:

ae-sample-identity-webapp.Dockerfile


### Usage

Follow these steps to build and run the application using Docker:

1.  **Build the Docker image:**
    Open your terminal in the same directory as `ae-sample-identity-webapp.Dockerfile` and run:
    ```bash
    docker build -t aesamples/ae-sample-identity-webapp -f ae-sample-identity-webapp.Dockerfile .
    ```

2.  **Run the Docker container:**
    * To run interactively (logs shown in the terminal, container removed on stop):
        ```bash
        docker run -it --rm -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp
        ```
    * To run in detached mode (runs in the background):
        ```bash
        docker run -d -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp
        ```

3.  **Access the application:**
    Open your web browser and navigate to `http://localhost:8000`.

4.  **Login Credentials:**
    Use the following credentials to log in:
    * **Email:** `info@softaren.com`
        **Password:** `Demo`
    * **Email:** `notifications@softaren.com`
        **Password:** `Demo`inerization: