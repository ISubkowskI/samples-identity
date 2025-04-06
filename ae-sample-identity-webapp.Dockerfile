# ae-sample-identity-webapp.Dockerfile
ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release

# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY "ae-sample-identity-webapp/*.csproj" "ae-sample-identity-webapp/"
COPY "ae-sample-identity-lib/*.csproj" "ae-sample-identity-lib/"
RUN dotnet restore "ae-sample-identity-webapp/ae-sample-identity-webapp.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ae-sample-identity-webapp"
RUN dotnet build "ae-sample-identity-webapp.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# --- Publish Stage ---
FROM build-env AS publish-env
RUN dotnet publish "ae-sample-identity-webapp.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS runtime
WORKDIR /app
COPY --from=publish-env /app/publish .

# Configure Kestrel to listen ONLY on HTTP port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Expose the port
EXPOSE 8080

ENTRYPOINT [ "dotnet", "ae-sample-identity-webapp.dll" ]