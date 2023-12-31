sudo cp ./scripts/ms-repo.pref /etc/apt/preferences.d/
sudo wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install azure-functions-core-tools-4
sudo rm -rf packages-microsoft-prod.deb
sudo apt install dotnet-sdk-6.0 -y
npm install -g @azure/static-web-apps-cli
dotnet tool install -g microsoft.sqlpackage
dotnet new -i Microsoft.Build.Sql.Templates
dotnet tool install --global Microsoft.DataApiBuilder --version 0.9.6-rc
sudo apt-get update
sudo apt-get install sqlcmd
sudo wget https://github.com/microsoft/go-sqlcmd/releases/download/v1.4.0/sqlcmd-v1.4.0-linux-amd64.tar.bz2
sudo bunzip2 sqlcmd-v1.4.0-linux-amd64.tar.bz2
sudo tar xvf sqlcmd-v1.4.0-linux-amd64.tar
sudo mv sqlcmd /usr/bin/sqlcmd
sudo rm -rf sqlcmd-v1.4.0-linux-amd64.tar
sudo rm -rf sqlcmd_debug
sudo rm -rf NOTICE.md
echo 'PATH=$PATH:$HOME/.dotnet/tools' >> ~/.bashrc
echo "rm packages-microsoft-prod.deb"
rm packages-microsoft-prod.deb
echo "rm microsoft.gpg"
rm microsoft.gpg
ls
mkdir -p /home/vscode/.swa/dataApiBuilder/0.9.6-rc/
cd /home/vscode/.swa/dataApiBuilder/0.9.6-rc/
wget https://github.com/Azure/data-api-builder/releases/download/v0.9.6-rc/dab_linux-x64-0.9.6-rc.zip
unzip dab_linux-x64-0.9.6-rc.zip
chmod 777 *

