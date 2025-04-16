Vagrant.configure("2") do |config|
  config.vm.box = "generic/ubuntu2204"
  config.ssh.insert_key = false

  config.vm.provider "virtualbox" do |vb|
    vb.memory = "2048"
    vb.cpus = 2
  end

  # Install Docker and add vagrant user to docker group
  config.vm.provision "shell", inline: <<-SHELL
    sudo apt-get update
    sudo apt-get install -y docker.io
    sudo usermod -aG docker vagrant
  SHELL

  # Shared folders
  config.vm.synced_folder "./code", "/home/vagrant/code", type: "virtualbox"
  config.vm.synced_folder "./output", "/home/vagrant/output", type: "virtualbox"
end
