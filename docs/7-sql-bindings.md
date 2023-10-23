![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Azure SQL bindings for Azure Functions

In this section, you will create a change data stream using Change Tracking, the Azure SQL Database, and the Azure SQL bindings for Azure Functions. The trigger will fire when an insert, update or delete is done on a table in the database. Once that change happens, the SQL binding will pick up the row and pass it to our Azure Function. In this lab, it will just display the row to the console log, but you can see how other Azure services like Event Hub, Blob storage, or Stream Processing can be used for event-based architecture application development.

## SQL Bindings workshop tasks

### Enable change tracking

1. In the SQL Server extension, right click on the Free Azure Database connection profile and select New Query.

    ![A picture of right clicking the Free Azure Database profile name and selecting New Query](./media/ch7/bind1a.png)

1. The person table needs change tracking enabled for the SQL bindings trigger to work correctly.
    The first command to be run will enable change tracking in the database.

    ```SQL
    ALTER DATABASE CURRENT
    SET CHANGE_TRACKING = ON;
    GO
    ```

    ![A picture of enabling change tracking on the current database](./media/ch7/bind1b.png)

    then enable change tracking on the person table:

    ```SQL
    ALTER TABLE [dbo].[person] ENABLE CHANGE_TRACKING;
    GO
    ```

    ![A picture of enabling change tracking on the person table](./media/ch7/bind1c.png)

### Create an Azure Function

1. Back in the terminal at the bottom of the page,

    ![A picture of the terminal at the bottom of the codespace window ](./media/ch7/bind1.png)  

    issue the following command to change the directory back to the top level of this project:

    ```bash
    cd /workspaces/azure-sql-db-developers-workshop
    ```

1. Next, issue the following command to start the function creation process:

    ```bash
    func init triggerBinding --worker-runtime dotnet
    ```

1. When this process is finished, click the File Explorer extension to see the new files that were created.

    ![A picture of clicking the File Explorer extension to see the new files that were created when creating the function project](./media/ch7/bind6.png)  

### Adding libraries to the project

1. Next, we need to add a package reference to the project (for Azure SQL Bindings). The following command will add this reference to the triggerBinding.csproj file.

    Run the following command in the terminal:

    ```bash
    cd triggerBinding
    ```

    then

    ```bash
    dotnet add package Microsoft.Azure.WebJobs.Extensions.Sql --prerelease
    ```

### Create the SQL trigger function

1. The next step is to create an Azure Function. Start by **pressing F1 or Shift-Ctrl-P** to bring up the command palette.

    ![A picture of pressing F1 or Shift-Ctrl-P to bring up the command palette](./media/ch7/bind9.png)  

1. Enter **create function** into the text field and then select **Azure Functions: Create Function**.

    ![A picture of entering create function into the text field and then selecting Azure Functions: Create Function](./media/ch7/bind10.png)  

1.  A dialog box will appear asking to **“Select a template for your function”**. Go to the bottom of the list and select **"Change template filter"**.

    ![A picture of going to the bottom of the template list and selecting Change template filter](./media/ch7/bind11.png)

1.  In the **“Select a template filter”** dialog box, **"All"**.

    ![A picture of selecting all in the Select a template filter dialog box](./media/ch7/bind12.png)

1.  Returning to the **“Select a template for your function”** dialog box, enter **"sql"** into the text field and select **SqlTriggerBinding**

    ![A picture of entering SQL into the text field in the Select a template for your function dialog box and select **SqlTriggerBinding](./media/ch7/bind13.png)

1. On the next step, "Create new SqlTriggerBinding (2/5)", keep the default value of **SqlTriggerBindingCSharp1** and press enter.

    ![A picture of keeping the default value of SqlTriggerBindingCSharp1 and pressing enter](./media/ch7/bind14.png)

1. On step 3 of the Create new SqlTriggerBinding flow, keep the default namespace of **"Company.Function"** and press enter.

    ![A picture of keeping the default namespace of Company.Function and pressing enter](./media/ch7/bind15.png)

1. On step 4 of the Create new SqlTriggerBinding flow, use the value of **"connection-string"** for the app setting name for the SQL database connection and press enter.

    ![A picture of using the value of connection-string for the app setting name for your SQL Connection and pressing enter](./media/ch7/bind16.png)

1. And in step 5, use the value of **"[dbo].[person]"** for the database table name that the SQL Binding trigger will watch (and the table that Change Tracking was enabled previously). Then press enter.

    ![A picture of use the value of [dbo].[person] for the database table name then pressing enter](./media/ch7/bind17.png)

1. The SqlTriggerBindingCSharp1.cs file has been created and is in the editor for review.

    ![A picture of the SqlTriggerBindingCSharp1.cs file](./media/ch7/bind18.png)

### Testing the trigger

1. Now that the function code is done, we need to provide it a value for SqlConnectionString. This variable can be placed in the local.settings.json file and contain the connect string for our locally running database.

    Open the local.settings.json file 

    ![A picture of ](./media/ch7/bind17.png)  

    and add the following line just below the “Values”: { section:

    ```JSON
    "SqlConnectionString": "Server=localhost,1433;Database=devDB;User ID=vscode;Password=XXXXX;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;",
    ```

    ![A picture of ](./media/ch7/bind18.png)  

    and be sure to replace the XXXXX with the password of the database that was created before. If you forgot the password, run the following in the terminal to find it again:

    ```bash
    sqlcmd config connection-strings
    ```

    And remember to SAVE the file when done.

1. At the terminal run the following command to start the Azure Function:

    ```bash
    func host start
    ```

    ![A picture of ](./media/ch7/bind19.png)  

    and once the function is started, open a new query sheet for the Local Database.

    ![A picture of ](./media/ch7/bind20.png)  

1. Issue the following SQL insert statement:

    ```SQL
    insert into dbo.person values(N'Ellie', N'ellie@contoso.com', N'Cats');
    ```

    You should see the following in the terminal window indicating the trigger binding did see the change:

    ```bash
    [2023-07-11T19:18:03.124Z] Executing 'changeDataStream' (Reason='New change detected on table '[dbo].[person]' at 2023-05-11T19:18:03.0902305Z.', Id=541ff09e-54ac-48e8-8d17-bbcc9a451432)
    [2023-07-11T19:18:03.176Z] Insert{"person_id":6,"person_name":"Ellie","person_email":"ellie@contoso.com","pet_preference":"Cats"}
    [2023-07-11T19:18:03.195Z] Executed 'changeDataStream' (Succeeded, Id=541ff09e-54ac-48e8-8d17-bbcc9a451432, Duration=93ms)
    ```
