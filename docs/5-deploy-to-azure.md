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

1. Next, in the **Database details** section of the page, name your database with the **Database name** field.

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

1. In the **Firewall rules** section, click the button labeled "Add your client IPv4 address (X.X.X.X)" to add your local IP address for database access.

    ![A picture of clicking the button labeled "Add your client IPv4 address (X.X.X.X)" to add your local IP address for database access](./media/ch5/deploy1v.png)

1. Click the **checkbox** for **Allow Azure services and resources to access this server** in the **Exceptions** section.

    ![A picture of clicking the checkbox for Allow Azure services and resources to access this server in the Exceptions section](./media/ch5/deploy1w.png)

1. Finally, click the **Save** button in the lower left of the page.

    ![A picture of clicking the save button in the lower left of the page](./media/ch5/deploy1x.png)

### Create a connection profile

1. Using the extensions panel, select the **SQL Server connections extension**

    ![A picture of selecting the SQL Server connection extension in code spaces](./media/ch5/deploy1.png)

    and create a new connection by clicking the plus sign on the upper right in the extension.

    ![A picture of creating a new connection by clicking the plus sign on the upper right in the extension](./media/ch5/deploy2.png)

1. Use the following values for the Create Connection dialog boxes:

    * Use “**vslivedemo.database.windows.net**” as the server name, then press Enter.
        ![A picture of using localhost as the server name](./media/ch5/deploy3.png)

    * Use “**vslivedemo**” as the database name, then press Enter.
        ![A picture of using vslivedemo as the database name](./media/ch5/deploy4.png)

    * In the Authentication Type dialog box, select “SQL Login“.
        ![A picture of using SQL Login as as the Authentication Type](./media/ch5/deploy5.png)

    * Refer back to the credentials supplied to you for the username and password. It would be in the format of “vsliveuser**X**” as the user name, **replacing X with the number of the database user you were given**

        In the User name (SQL Login) dialog box, enter the user from the supplied credentials, then press Enter.
        ![A picture of entering vsliveuserX as the database user](./media/ch5/deploy6.png)

        and provide the password from the supplied credentials in the Password (SQL Login) dialog box, then press Enter.
        ![A picture of entering the password for the database user](./media/ch5/deploy7.png)

    * Select “Yes” so that the password is saved (encrypted) on the connection profile
        ![A picture of selecting Yes so that the password is saved on the connection profile](./media/ch5/deploy8.png)

    * Provide the profile name of "Azure Database" in the last dialog box for this step. Press Enter to finish the connection profile process.
        ![A picture of using Azure Database as the connection profile name](./media/ch5/deploy9.png)

    * After pressing Enter and the connection profile is verified, a warning box **may** appear on the lower right of the screen. This warning is indicating that due to new security features within the database, you need to enable the self-signed certificate.
        Click the Enable Trust Server Certificate green button to continue.

        ![A picture of clicking the Enable Trust Server Certificate green button to continue](./media/ch5/deploy10.png)

    * There is now a connection to the Azure SQL Database running in the cloud in the code space you can use for deployment and further development.

        ![A picture of code spaces indicating a successful connection](./media/ch5/deploy11.png)

### Create a post deployment script

1. Return to the Database Projects extension, 

    ![A picture of right clicking the database project extension](./media/ch5/deploy12.png)


    and right click the project name (devDB) and select **Add Post-Deployment Script**

    ![A picture of right clicking the project name and selecting Add Post-Deployment Script](./media/ch5/deploy13.png)

1. Keep the default name of **Script.PostDeployment1** and press enter/return.

    ![A picture of keeping the default name of Script.PostDeployment1 for the Post-Deployment Script](./media/ch5/deploy14.png)


1. Replace the files contents with the following SQL statements

    ```SQL
    insert into person(person_name, person_email, pet_preference) values('Bill','bill@contoso.com','Dogs');
    insert into person(person_name, person_email, pet_preference) values('Frank', 'frank@contoso.com','Cats');
    insert into person(person_name, person_email, pet_preference) values('Riley', 'Riley@contoso.com','Cats');
    insert into address (person_id, address) values (1, 'Lincoln, MA');
    insert into address (person_id, address) values (2, 'Baltimore, MD');
    GO
    ```

    and **SAVE** the file.

    ![A picture of replacing the files contents with insert SQL statements](./media/ch5/deploy15.png)

### Publish to an Azure SQL Database

1. To publish the database code to the local database, **right click** on the project and select **Publish**.

    ![A picture of right clicking the project name and selecting Publish](./media/ch5/deploy16.png)

1. Select "Publish to an existing Azure SQL logical server" on the first step

    ![A picture of selecting Publish to an existing Azure SQL logical server in the publish database modal flow](./media/ch5/deploy17.png)

1. Next, select "Don't use profile"

    ![A picture of selecting Don't use profile in the publish database modal flow](./media/ch5/deploy18.png)

1. Choose the **Azure Database** connection you created previously for the connection profile step

    ![A picture of selecting the Azure Database connection in the publish database modal flow](./media/ch5/deploy19.png)

1. Choose **vslivedemo** as the database.

    ![A picture of selecting the database named devDB in the publish database modal flow](./media/ch5/deploy20.png)

1. And finally, for the action, choose **Publish**.

    ![A picture of selecting Publish in the publish database modal flow](./media/ch5/deploy21.png)

1. Once the dacpac is published into the database,

    ![A picture of the dacpac being published to the database successfully](./media/ch5/deploy22.png)

    you can go to the SQL Server Connection extension, reload the database objects by right clicking it and selecting refresh,

    ![A picture of reloading the database objects by right clicking it and selecting refresh in the SQL Server extension](./media/ch5/deploy23.png)

     then open the Tables and Programmability folders to see the deployed objects.

    ![A picture of the Tables and Programmability folders to and the deployed objects](./media/ch5/deploy24.png)

### Verify the deployed database objects and data

1. While still on the **SQL Server Connections extension**, right click the database profile name,** Azure Database**, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the Azure Database profile name and selecting New Query](./media/ch5/deploy25.png)

1. Run the following code in the query sheet:

    ```SQL
    select * from person;
    select p.person_name, a.address
    from person p, address a
    where p.person_id = a.person_id;
    go
    ```

1. You can also test out the stored procedure with the following code:

    ```SQL
    exec get_person_by_pet 'Dogs';
    ```
