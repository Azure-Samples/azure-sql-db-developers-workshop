![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Deploy the database objects to the Azure SQL Database

The next section of the workshop will be using an Azure SQL Database. To move our development environment into the cloud, all that is needed to be done is use SQL Database Projects and point it at an Azure SQL Database. The deploy feature will then create the objects in the selected cloud database just as it did with the local SQL Server database in the code space.

## Deploy to Azure workshop tasks

### Create a free Azure SQL Database

1. Ensure you have an Azure account to log into the Azure Portal. Need a free account? Sign up for one [here](https://azure.microsoft.com/en-us/free).

1. Navigate to the [Azure Portal](https://portal.azure.com/#home), and in the upper left corner, click the menu button.

    ![A picture of selecting the menu in the upper left corner of the Azure Portal](./media/ch5/deploy1a.png)

1. Then, select **Create a Resource**.

    ![A picture of selecting Create a Resource from the Azure Portal menu](./media/ch5/deploy1b.png)

1. In the category menu, select **Databases**.

    ![A picture of selecting Database from the category menu](./media/ch5/deploy1c.png)

1. Then click **create** for **SQL Database**.

    ![A picture of selecting create for SQL Database](./media/ch5/deploy1d.png)

1. On the **Create SQL Database** page, click the **Apply offer (Preview)** button for the free Azure SQL Database.

    ![A picture of clicking the Apply offer (Preview) button for the free Azure SQL Database on the Create SQL Database page](./media/ch5/deploy1e.png)

1. In the **Project details** section of the page, select a subscription and a Resource group if you have an existing one. 

    ![A picture of select a subscription and a Resource group on the Project details section of the page](./media/ch5/deploy1f.png)

    Otherwise you can create a Resource group by clicking the **Create new** button.

    ![A picture of creating a Resource group by clicking the **Create new** button](./media/ch5/deploy1g.png)

1. Next, in the **Database details** section of the page, name your database **freedb** with the **Database name** field.

    ![A picture of naming your database with the Database name field in the Database details section of the page](./media/ch5/deploy1h.png)

1. For the **Server**, click the **Create new** button.

    ![A picture of naming your database with the Database name field in the Database details section of the page](./media/ch5/deploy1i.png)

1. On the **Create SQL Database Server** page, enter a **server name** and choose a **Location** using the dropdown menu.

    ![A picture of entering a server name and choosing a Location using the dropdown menu on the Create SQL Database Server page](./media/ch5/deploy1j.png)

1. Now, in the **Authentication** section, select the **radio button** for **Use both SQL and Microsoft Entra authentication**.

    ![A picture of selecting the radio button for Use both SQL and Microsoft Entra authentication in the authentication section](./media/ch5/deploy1k.png)

1. Click the **Set admin** link in the **Set Microsoft Entra admin** section. 

    ![A picture of clicking the Set admin link in the Set Microsoft Entra admin section](./media/ch5/deploy1l.png)

1. Using the **Microsoft Entra ID** blade, find your user account and select it as an admin. Then click the **Select** button on the bottom left.

    ![A picture of select your account as the Entra ID admin](./media/ch5/deploy1m.png)

1. Set the **Server admin login** as sqladmin and then enter a strong password for the password fields.

    ![A picture of setting the Server admin login as sqladmin and then entering a strong password for the password fields](./media/ch5/deploy1n.png)

1. Click the **OK** button on the bottom left of the page.

1. Back on the **Create a SQL Database** page, verify the values you entered and that the free database offer has been applied. Then click the **Review + create** button in the lower left of the page.

    ![A picture of verifying the values and that the free database offer has been applied and clicking the Review + create button in the lower left of the page](./media/ch5/deploy1o.png)

1. On the following page, click the **Create** button in the lower left.

    ![A picture of clicking the Create button in the lower left](./media/ch5/deploy1p.png)

1. The following page will detail the deployments progress.

    ![A picture of the database deployment progress page](./media/ch5/deploy1q.png)

1. Once the deployment is done, click the blue **Go to resource** button to see your database details.

    ![A picture of clicking the blue Go to resource button to see your database details on the database deployment progress page](./media/ch5/deploy1r.png)

#### Network access to the database

1. On the database details page, the right hand side, you will see the **Server name** field with a link the your database server. Click the server name link.

    ![A picture of clicking the server name link on the database details page](./media/ch5/deploy1s.png)

1. Click the **Networking** link on the left hand side menu in the **Security** section.

    ![A picture of clicking the Networking link on the left hand side menu in the Security section](./media/ch5/deploy1t.png)

1. On the **Networking** page, click the **radio button** next to **Selected networks**.

    ![A picture of clicking the radio button next to Selected networks on the networking page](./media/ch5/deploy1u.png)

1. In the **Firewall rules** section, click the button labeled **"Add your client IPv4 address (X.X.X.X)"** to add your local IP address for database access.

    ![A picture of clicking the button labeled "Add your client IPv4 address (X.X.X.X)" to add your local IP address for database access](./media/ch5/deploy1v.png)

1. Finally, click the **Save** button in the lower left of the page.

    ![A picture of clicking the save button in the lower left of the page](./media/ch5/deploy1x.png)

### Create a connection profile to the free Azure SQL Database

1. While still in the Azure Portal, using the menu on the left side, open the menu items under **Settings** if not already opened. Then select **SQL Databases** by clicking on it.

    ![A picture of clicking the Azure SQL Databases option under settings](./media/ch5/deploy2a.png)

1. Next, on the main page, find the freedb you created and click on it navigate to the database details page.

    ![A picture of clicking the freedb SQL Databases](./media/ch5/deploy2b.png)

1. On the database details page, using the menu bar on the left, find and click on **Connection strings** under the **Settings** menu. You may have to expand settings to find Connection strings under it.

    ![A picture of finding and clicking on Connection strings under the Settings menu](./media/ch5/deploy2c.png)

1. On the **ADO.NET** tab, find the **ADO.NET (SQL authentication)** entry and copy that connect string.

    ![A picture of finding the ADO.NET (SQL authentication) entry and copying that connect string](./media/ch5/deploy2d.png)

1. Back in the codespace, using the extensions panel, select the **SQL Server connections extension**

    ![A picture of selecting the SQL Server connection extension in code spaces](./media/ch5/deploy1.png)

    and create a new connection by clicking the plus sign on the upper right in the extension.

    ![A picture of creating a new connection by clicking the plus sign on the upper right in the extension](./media/ch5/deploy2.png)

1. In the Create Connection dialog box, **paste** in the **ADO.NET (SQL authentication)** connect string you copied. **Make sure you change the username and password to be the values you used when creating the database. (sqladmin and password)**

    ![A picture of using the ADO.NET (SQL authentication)** connect string you copied](./media/ch5/deploy3.png)

1. Provide the profile name of "Free Azure Database" in the last dialog box for this step. Press Enter to finish the connection profile process.

    ![A picture of using Free Azure Database as the connection profile name](./media/ch5/deploy9.png)

    * After pressing Enter and the connection profile is verified, a warning box **may** appear on the lower right of the screen. This warning is indicating that due to new security features within the database, you need to enable the self-signed certificate.
        Click the Enable Trust Server Certificate green button to continue.

        ![A picture of clicking the Enable Trust Server Certificate green button to continue](./media/ch5/deploy10.png)

    * There is now a connection to the Azure SQL Database running in the cloud in the code space you can use for deployment and further development.

        ![A picture of code spaces indicating a successful connection](./media/ch5/deploy11.png)

### Publish to an Azure SQL Database

1. To publish the database code to the Free Azure SQL Database, **right click** on the project and select **Publish**.

    ![A picture of right clicking the project name and selecting Publish](./media/ch5/deploy16.png)

1. Select "Publish to an existing Azure SQL logical server" on the first step

    ![A picture of selecting Publish to an existing Azure SQL logical server in the publish database modal flow](./media/ch5/deploy17.png)

1. Next, select "Don't use profile"

    ![A picture of selecting Don't use profile in the publish database modal flow](./media/ch5/deploy18.png)

1. Choose the **Free Azure Database** connection you created previously for the connection profile step

    ![A picture of selecting the Free Azure Database connection in the publish database modal flow](./media/ch5/deploy19.png)

1. Choose **freedb** as the database.

    ![A picture of selecting the database named freedb in the publish database modal flow](./media/ch5/deploy20.png)

1. And finally, for the action, choose **Publish**.

    ![A picture of selecting Publish in the publish database modal flow](./media/ch5/deploy21.png)

1. Once the dacpac is published into the database,

    ![A picture of the dacpac being published to the database successfully](./media/ch5/deploy22.png)

    you can go to the SQL Server Connection extension, reload the database objects by right clicking it and selecting refresh,

    ![A picture of reloading the database objects by right clicking it and selecting refresh in the SQL Server extension](./media/ch5/deploy23.png)

     then open the Tables and Programmability folders to see the deployed objects.

    ![A picture of the Tables and Programmability folders to and the deployed objects](./media/ch5/deploy24.png)

### Verify the deployed database objects and data

1. While still on the **SQL Server Connections extension**, right click the database profile name,**Free Azure Database**, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the Free Azure Database profile name and selecting New Query](./media/ch5/deploy25.png)

1. Run the following code in the query sheet:

    ```SQL
    select * from dbo.person;

    select p.person_name, a.address
    from dbo.person p inner join dbo.[address] a
    on p.person_id = a.person_id;
    
    select * from dbo.todo;
    go
    ```

1. You can also test out the stored procedure with the following code:

    ```SQL
    exec get_person_by_pet 'Dogs';
    ```

## Would you like to know more?

### Azure SQL migration extension for Azure Data Studio

The Azure SQL migration extension for Azure Data Studio enables you to assess, get right-sized Azure recommendations and migrate your SQL Server databases to Azure. You can use the Azure SQL Migration extension to assess SQL Server databases running on Windows or Linux.

The Azure SQL Migration extension for Azure Data Studio offers these key benefits:

* A responsive UI for an end-to-end migration experience. The extension starts with a migration readiness assessment and SKU recommendation (preview) (based on performance data).
* An enhanced assessment mechanism that can evaluate SQL Server instances. The extension identifies databases that are ready to migrate to Azure SQL targets.
* An SKU recommendation engine that collects performance data from the on-premises source SQL Server instance and then generates right-sized SKU recommendations based on your Azure SQL target.
* A reliable Azure service powered by Azure Database Migration Service that orchestrates data movement activities to deliver a seamless migration experience.
* You can run your migration online (for migrations that require minimal downtime) or offline (for migrations where downtime persists throughout the migration) depending on your business requirements.
* You can configure a self-hosted integration runtime to use your own compute resources to access the source SQL Server instance backup files in your on-premises environment.
* Provides a secure and improved user experience for migrating TDE databases and SQL/Windows logins to Azure SQL.

### Proof of concepts and samples

* [SQL Server migration one-click PoC to Azure SQL](https://aka.ms/SQLMigrationPoC)
* [Migrate databases at scale using Azure Database Migration automation](https://github.com/Azure-Samples/data-migration-sql)

## Continue to chapter 6

Click [here](./6-JSON-in-the-db.md) to continue to chapter 6, JSON in the database!