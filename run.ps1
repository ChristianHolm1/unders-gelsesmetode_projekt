#!/bin/bash

# Step 1: Start the virtual machine and provision it (vagrant up)
echo "Starting the VM..."
vagrant up

echo "Building the Docker image..."
vagrant ssh -c "cd /vagrant && docker build -t csharp-app ."

# Step 4: Run the Docker container
echo "Running the Docker container..."
vagrant ssh -c "docker run --rm csharp-app"

# Step 5: Optionally shut down the VM after running the container
echo "Shutting down the VM..."
vagrant halt