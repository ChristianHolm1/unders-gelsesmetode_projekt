# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the project files
COPY code/*.sln ./
COPY code/ ./code/

# Restore dependencies
WORKDIR /app/code
RUN dotnet restore

# Build and publish the project
RUN dotnet publish -c Release -o /app/publish --no-restore

# Use the .NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory
WORKDIR /app

# Copy the published output from the build environment
COPY --from=build-env /app/publish ./

# Command to run the application
CMD ["dotnet", "ConsoleApp1.dll"]
