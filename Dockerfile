# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy solution and project files
COPY code/*.sln ./
COPY code/ ./code/

# Install necessary NuGet packages
WORKDIR /app/code
RUN dotnet add package BenchmarkDotNet \
    && dotnet add package ClosedXML \
    && dotnet add package Newtonsoft.Json

# Restore and publish in Release mode
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore

# Use the .NET runtime image for final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory in the container
WORKDIR /app

# Copy the published output
COPY --from=build-env /app/publish ./

# Run the app
CMD ["dotnet", "LINQ_Benchmarking.dll"]
