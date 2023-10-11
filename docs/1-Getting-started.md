![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Getting started with your development environment

For this workshop, we will be using GitHub codespace. This environment will provide us with all the necessary tools to run this lab completely in a cloud development space.

## Clone/fork repository with necessary files

* Repository at https://github.com/Azure-Samples/azure-sql-db-developers-workshop/

### Repository Contents

1. .devcontainer
    1. Codespace docker config
1. client
    1. contains the Todo application for use with Data API builder and SWA
1. docs
    1. Has all the chapters/lessons of the workshop
1. scripts
    1. Installs all the needed dev tools

### Free accounts needed for the workshop

1. GitHub account
1. Azure account
    1. Free Azure SQL Database

## Install necessary software components or start codespace with specialized container

### If using codespace:

1. Start/Create a codespace from the forked repository. Click on the **green Code button**, then click the **Create codespace on master** button.

    ![A picture of clicking on the green Code button, then clicking the Create codespace on master button](./media/ch1/start1.png)

    and the codespace will start setting itself up

    ![A picture of the codespace setting up](./media/ch1/start2.png)

1. After the codespace editor window appears, in a minute or so, the post-scripts will start installing the necessary extensions, plugins and software needed for this workshop.

    ![A picture of the post create scripts running](./media/ch1/start3.png)

1. Once the scripts have finished, you will be brought back to a prompt in the codespace terminal

    ![A picture of a prompt in the codespace terminal](./media/ch1/start4.png)

### If on the desktop/local environment

1. If using your local machine for this workshop, please ensure you have all the listed software installed and the repository cloned/downloaded.

* [VS Code](https://code.visualstudio.com/)
* [Azure Data Studio](https://learn.microsoft.com/sql/azure-data-studio/download-azure-data-studio)
* [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash#install-the-azure-functions-core-tools)
* [.NET SDK 6+](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Go-sqlcmd](https://github.com/microsoft/go-sqlcmd)
* [SqlPackage](https://learn.microsoft.com/sql/tools/sqlpackage/sqlpackage-download)
* [Data API Builder](https://github.com/Azure/data-api-builder)
* [Docker desktop](https://www.docker.com/products/docker-desktop/)
