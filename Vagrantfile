Vagrant.configure("2") do |config|
  config.vm.box = "generic/ubuntu2204"
  config.ssh.insert_key = true

  config.vm.provider "virtualbox" do |vb|
    vb.customize ["modifyvm", :id, "--cableconnected1", "on"]
    vb.memory = "2048"
    vb.cpus = 2
  end

  config.vm.provision "shell", inline: <<-SHELL
    # Update and install dependencies
    sudo apt-get update
    sudo apt-get install -y apt-transport-https ca-certificates curl software-properties-common lsb-release

    # Install Microsoft package signing key and feed for .NET 8
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /etc/apt/trusted.gpg.d/microsoft.gpg
    sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" > /etc/apt/sources.list.d/microsoft-dotnet.list'

    # Install .NET 8 SDK
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-8.0

    # Verify installation
    dotnet --version
  SHELL

  # Shared folders
  config.vm.synced_folder "./code", "/home/vagrant/code", type: "virtualbox"
end
