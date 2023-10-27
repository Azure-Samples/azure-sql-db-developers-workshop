![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# CI/CD with GitHub Actions

[GitHub Actions]() is a continuous integration and continuous delivery (CI/CD) platform that allows you to automate your build, test, and deployment pipeline. You can create workflows that build and test every pull request to your repository, or deploy merged pull requests to production. GitHub Actions goes beyond just DevOps and lets you run workflows when other events happen in your repository. For example, you can run a workflow to automatically add the appropriate labels whenever someone creates a new issue in your repository.

This chapter will be using the [sql-action GitHub Action](https://github.com/Azure/sql-action) for deploying changes to Azure SQL or SQL Server in a dacpac, SQL scripts, or an SDK-style SQL project. This action will automate a workflow to deploy updates to Azure SQL or SQL Server on a repository push.

https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions

https://github.com/actions/checkout
https://github.com/Azure/sql-action


## GitHub Actions workshop tasks

### Create the workflows directory

### Create the .yml file

### Test the workflow with a push