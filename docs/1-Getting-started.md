![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Getting started with your development environment

For this workshop, we will be using GitHuv code space. This environment will provide us with all the necessary tools to run this lab completely in a cloud development space.

## Clone/fork repository with necessary files

* Repository at https://github.com/Azure-Samples/XXXXX
* Private working repository at https://github.com/JetterMcTedder/azure-sql-db-developers-workshop

This will also bring with it a pre-configured code spaces with some tools pre-installed

### Repository Contents

1. Scripts
    1. Installs all the needed dev tools
1. TBD
1. TBD
1. .devcontainer
    1. Codespaces docker config

### Create free accounts

1. Azure account
1. GitHub account

## Install necessary software components or start code spaces with specialized container

### If using Code Spaces:

Done via sh script we put into the devcontainer.json

```bash
{
  "image": "mcr.microsoft.com/devcontainers/dotnet:7.0",
"features": {
	"ghcr.io/devcontainers-contrib/features/npm-package:1": {
		"package": "typescript",
		"version": "latest"
	},
	"ghcr.io/devcontainers/features/node:1": {
		"nodeGypDependencies": true,
		"version": "latest"
	},
	"ghcr.io/devcontainers/features/docker-in-docker:2": {
		"moby": true,
		"azureDnsAutoDetection": true,
		"installDockerBuildx": true,
		"version": "latest",
		"dockerDashComposeVersion": "v1"
	}
  },
"postCreateCommand": "bash scripts/install-dev-tools.sh",
"customizations": {
	"vscode": {
		"extensions": [
			"ms-mssql.sql-database-projects-vscode",
			"ms-azuretools.vscode-azurefunctions",
			"ms-vscode.azure-account",
			"ms-azuretools.vscode-azureresourcegroups"
		]
	}
}
}
```

Script contains:

```bash
dotnet tool install -g microsoft.sqlpackage
dotnet new install Microsoft.Build.Sql.Templates
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install azure-functions-core-tools-4
sudo apt install dotnet-sdk-6.0 -y
npm install -g @azure/static-web-apps-cli
dotnet tool install --global Microsoft.DataApiBuilder
sudo apt-get update
sudo apt-get install sqlcmd
dab --version 
func version 
dotnet --version
echo "rm packages-microsoft-prod.deb"
rm packages-microsoft-prod.deb
echo "rm microsoft.gpg"
rm microsoft.gpg
ls
```

### If on the desktop/local environment (we can put these links/requirements out before the workshop) 

* [VS Code](https://code.visualstudio.com/)
* [Azure Data Studio](https://learn.microsoft.com/sql/azure-data-studio/download-azure-data-studio)
* [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash#install-the-azure-functions-core-tools)
* [.NET SDK 6+](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Go-sqlcmd](https://github.com/microsoft/go-sqlcmd)
* [SqlPackage](https://learn.microsoft.com/sql/tools/sqlpackage/sqlpackage-download)
* [Data API Builder](https://github.com/Azure/data-api-builder)
* [Docker desktop](https://www.docker.com/products/docker-desktop/)

After desktop install, have series of checks to ensure its installed and ready (Winget listing for example)

```bash
dab --version
func version
dotnet --version
```
