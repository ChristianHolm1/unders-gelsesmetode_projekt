# Use the official .NET 8 SDK image (this is now the FINAL image too!)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

# Set working directory
WORKDIR /app

# Copy everything into container
COPY . .

# Build project in Release mode
RUN dotnet build -c Release

# Run the benchmarks
ENTRYPOINT ["dotnet", "run", "--configuration", "Release", "--"]
