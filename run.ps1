#!/bin/bash

# Step 1: Start the virtual machine and provision it (vagrant up)
echo "Starting the VM..."
vagrant up

# Step 2: Sync code to a temporary build directory inside the VM
echo "Copying code into VM build folder..."
vagrant ssh -c "rm -rf ~/build && mkdir -p ~/build && cp -r /vagrant/code ~/build/code && cp /vagrant/Dockerfile ~/build"

# Step 3: Build the Docker image from the VM's internal folder (avoid /vagrant issues)
echo "Building the Docker image..."
vagrant ssh -c "cd ~/build && docker build -t csharp-app ."

# Step 4: Run the Docker container (mount output folder if needed)
echo "Running the Docker container..."
vagrant ssh -c "docker run --rm -v /vagrant/output:/app/output csharp-app"

# Step 5: Optionally shut down the VM after running the container
echo "Shutting down the VM..."
vagrant halt
