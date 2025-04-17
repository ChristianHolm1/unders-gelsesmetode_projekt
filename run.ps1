#!/bin/bash

# Step 1: Start the virtual machine
echo "🔧 Starting the VM..."
vagrant up

# Step 2: Build and run your .NET app
echo "🚀 Running benchmarks inside VM..."
vagrant ssh -c "cd code && dotnet run -c Release"

# Step 3: Shut down the VM
echo "🛑 Shutting down the VM..."
vagrant halt
