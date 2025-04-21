# 🔍 Benchmarking LINQ vs Traditional Algorithms

This project benchmarks the performance of LINQ-based sorting and searching versus traditional algorithms. To ensure consistent results across machines, everything runs inside a pre-configured virtual machine (VM) using **Vagrant** and **VirtualBox**.

---

## 🧰 Prerequisites

Before you begin, make sure you have the following software installed:

1. **[VirtualBox](https://www.virtualbox.org/wiki/Downloads)** – Used to run the virtual machine.
2. **[Vagrant](https://developer.hashicorp.com/vagrant/downloads)** – Automates VM setup.

---

## 📦 Cloning the Project

To get started, you'll need to download this project to your computer.

### Option 1: Using Git (recommended)

If you have Git installed, open a terminal or command prompt and run:

```bash
git clone https://github.com/your-username/benchmark-linq-vs-traditional.git
cd benchmark-linq-vs-traditional
```

### Option 2: Download ZIP

Click the green "Code" button on the GitHub page.

Choose "Download ZIP".

Extract the folder and open it in your terminal.

## 🚀 Running the Benchmarks

Once you're inside the project folder in your terminal, run the following commands:

```bash
#1. Start a virtual machine. (This step can take up to 5 minutes, if it's stuck, refere to troubleshooting)
vagrant up

#2. Run the benchmark tests inside the VM using .NET. (password: vagrant)
vagrant ssh -c "cd code && dotnet run -c Release"

#3. Shut the VM down when done.
vagrant halt
```
## 📁 Viewing the Results

After the VM finishes running, you'll find the benchmark results in the following folder:
```bash
code/results/
```
You can open this folder from your file explorer or directly from the terminal.

---

## 🛠 Troubleshooting

### 🔐 VM Stuck at "SSH auth method: private key"?

If the VM seems to hang or freeze at this message:

👉 Try resetting the VM by running:

```bash
vagrant destroy -f
vagrant up
