#!/bin/bash

# Step 1: Start the virtual machine
echo "🔧 Starting the VM..."
vagrant up

# Step 2: Build the Docker image from the shared /vagrant/code folder
echo "🐳 Building the Docker image..."
vagrant ssh -c "cd code && docker build -t csharp-app ."

# Step 3: Run the Docker container, mounting the shared /vagrant/output folder
echo "🚀 Running the Docker container..."
vagrant ssh -c "docker run --rm -v /vagrant/output:/app/output csharp-app"

# Step 4: Shut down the VM
echo "🛑 Shutting down the VM..."
vagrant halt
