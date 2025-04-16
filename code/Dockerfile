# Step 1: Use the official .NET SDK base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Step 2: Set working directory in the container
WORKDIR /app

## Step 3: Copy the solution and project files
COPY LINQ_Benchmarking.sln ./
COPY LINQ_Benchmarking/ ./LINQ_Benchmarking/
# Step 3: Optional - verify SDK is present
RUN dotnet --info

# Step 4: Copy the project files
COPY . .

# Step 5: Restore the project dependencies
RUN dotnet restore LINQ_Benchmarking.sln

# Step 6: Build the project
RUN dotnet build LINQ_Benchmarking.sln -c Release

# Step 7: Define entry point for the container
ENTRYPOINT ["dotnet", "run", "--project", "LINQ_Benchmarking/"]


