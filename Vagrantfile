# Vagrantfile for Linux virtual machine
Vagrant.configure("2") do |config|
  # Specify the base Linux box (Ubuntu 22.04 LTS)
  config.vm.box = "ubuntu/jammy64"
  config.ssh.insert_key = false

  # Set VM resources
  config.vm.provider "virtualbox" do |vb|
    vb.memory = "2048"      # 2 GB RAM
    vb.cpus = 2              # 2 CPU cores
  end

  # Provisioning: Install Docker
  config.vm.provision "shell", inline: <<-SHELL
    # Update package list
    sudo apt-get update

    # Install Docker
    sudo apt-get install -y docker.io

    # Add vagrant user to docker group (so you don't need sudo)
    sudo usermod -aG docker vagrant
  SHELL
end
