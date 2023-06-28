![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Workshop Overview

## Workshop Goals

![A picture of the flow of the workshop](./media/flow1.png)

* Learn how to create a full development environment locally or via Code Spaces
* Deploy a database and create tables with Visual Studio Code
* Utilize a Database Project for deployment and CI/CD flows
* Expose data in your database via REST and GraphQL with Data API Builder
* Native integration with Azure Functions and other Azure Services from the Azure SQL Database

## What you will be building

In today's workshop, you will be creating an environment for database application development. This environment will consist of a database, a framework to expose the database, and an application front end. You can accomplish this task in two ways. First, you can run every component locally on your laptop/desktop. In fact, you might even have most of the required components already installed and are using daily. The second method is to run this development environment in GitHub Code Spaces. By forking the repository, you can create a code space will all the necessary development tools preinstalled for you. Code Spaces even will allow docker in docker so that a development database can be created if needed.

The first lab will begin with database development. You will be creating tables and stored procedures to be used by our application. These tables will be created to include best practices such as primary keys/identity columns, correct data types, and foreign key relationships. By starting with a sound and well thought out data model, you can avoid costly mistakes as your application grows.

We will then expand our local development environments with Azure Functions and Static Web Applications. These frameworks will be installed and used locally but can be easily deployed into Azure.  Azure Functions allows you to implement application logic into scalable and serverless blocks of code to be run on demand. We can expose our database tables via the EF Core Framework with Azure Functions. The Static Web Applications framework will provide us with a mock deployment platform locally so we can utilize features such as authentication, functions and database integration.

In the third lab, you will be using the new Data API builder to create REST and GraphQL APIs into the database with a single command. You will then test these APIs with various code samples to see how simple it is to expose the database layer to applications.

XXX

The last lab will be taking the REST and GraphQL APIs you created in the previous labs and exposing them in applications. These applications will also utilize authorization and authentication to control what each user will see based on who they are and what roles they have been given.

## Architecture and components



Lab Overview presentation
What we going to do today/Why you are here
Jam Stack/Full Stack overview
Technologies in the stack
Why Jam Stack
Architecture overview in Azure/locally
Development Component Overview
.NET
Node
VS Code
Azure Functions
SWA/SWA cli
DAB/EF Core
AZ cli
GitHub and git
CICD/Development flow overview 