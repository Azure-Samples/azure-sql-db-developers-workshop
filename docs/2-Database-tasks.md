![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Database tasks

## Using Azure SQL Database and Visual Studio Code

This workshop will use SQL Server 2022 and the Azure SQL Database for the backend database. We can create a full instance of SQL Server 2022 locally with docker for free as well as create a free Azure SQL Database in the Azure Cloud; we will use both and seamlessly deploy our database objects with Database Projects from our local docker instance to the cloud.

The SQL Database gives us the ability to store data not only in the traditional relational database format (tables/rows/columns), but also in JSON as well as graph relationships. This flexibility of data storage and deployment options, all while being free to use gives developers a compelling option for their backend database. Azure SQL Database goes beyond a basic cloud database by being a modern, fully managed platform as a service (PaaS) that handles database management functions such as upgrading, patching, backups, and monitoring without developer involvement.

### SQL Database Projects and CI/CD

The SQL Database Projects extension is an Azure Data Studio and Visual Studio Code extension for developing SQL databases in a project-based development environment that sets you up for full CI/CD workflows. These database projects can be deployed into a new database creating development/test environments or in existing databases applying only the changes found in a particular deployment version or branch. Combine SQL Database Projects with GitHub actions to create a CI/CD flow for testing changes and correcting issues before they get into production environments by applying the changes to a database upon every commit or pull request.

## Database Workshop Tasks

### Create a SQL Database Project

1. In the codespace, at the terminal at the bottom of the page, return to the main directory

    ```bash
    cd /workspaces/azure-sql-db-developers-workshop
    ```

1. Using the codespace termnal, source the .bashrc file to uptake any changes the install may have made to the path

    ```bash
    . ~/.bashrc
    ```

1. Create the project directory in the terminal using the following command:

    ```bash
    mkdir database
    ```

1. Enter the directory

    ```bash
    cd database
    ```

1. And create a Database Project using a new templates via .NET

    ```bash
    dotnet new sqlproj -n "devDB" -tp "SqlAzureV12"
    ```

1. Use the side extensions panel in VS Code and select **SQL Database Projects**

    ![A picture of the side extensions panel in VS Code with SQL Database Projects highlighted](./media/ch2/database1.png)

    If your project is not automatically opened in the Database Projects extension, you can find it by clicking the **Open existing** green button

    ![A picture of clicking the Open existing green button](./media/ch2/database2.png)

    and then selecting the devDB.sqlproj file located at /workspaces/azure-sql-db-developers-workshop/database/devDB/ using the find file modal.

    ![A picture of selecting the devDB.sqlproj file located at /workspaces/codespace-for-DB-Devs/database/devDB/](./media/ch2/database3.png)

1. Next, using the terminal again, create a database locally with go-sqlcmd and Docker. This command will download the image from the Microsoft container repository and create a full SQL Server 2022 instance (Developer Edition) on your local machine or in the code space.

    ```bash
    sqlcmd create mssql -u devDB --accept-eula
    ```
    ![A picture of creating a database with sqlcmd](./media/ch2/database4.png)


1. Using sqlcmd, view the connection string information with the following command

    ```bash
    sqlcmd config connection-strings
    ```

    ![A picture of getting the database connection information with sqlcmd](./media/ch2/database5.png)

    Take note of the ADO.NET connection string. This will be used in the following steps.

1. Using the extensions panel, select the **SQL Server connections extension**.

    ![A picture of selecting the SQL Server connection extension in code spaces](./media/ch2/database6.png)

    and **create a new connection** by clicking either the **Add Connection** label or the **plus sign** on the upper right in the extension.

    ![A picture of creating a new connection by clicking either the Add Connection label or the plus sign on the upper right in the extension](./media/ch2/database7.png)

1. Copy the **ADO.NET connection string**

    ![A picture of copying the ADO.NET database connection information](./media/ch2/database7a.png)

    and paste it into the **Create Connection dialog** box:

   ![A picture of pasting the ADO.NET database connection information into the Create Connection dialog box](./media/ch2/database7b.png)

1. Provide the profile name of "Local Database" in the last dialog box for this step. Press Enter to finish the connection profile process.

    ![A picture of using Local Database as the connection profile name](./media/ch2/database14.png)

    * After pressing Enter and the connection profile is verified, a warning box **MAY** will appear on the lower right of the screen. This warning is indicating that due to new security features within the database, you need to enable the self-signed certificate.

        Click the Enable Trust Server Certificate green button to continue.

        ![A picture of clicking the Enable Trust Server Certificate green button to continue](./media/ch2/database15.png)

    * There is now a connection to this local database running in docker in the code space you can use for development.

        ![A picture of code spaces indicating a successful connection](./media/ch2/database16.png)

### Create two tables and a stored procedure for testing the sample JavaScript Application

1. Back in the Database Projects extension, right click the project name (devDB) and select **Add Table**

    ![A picture of right clicking the project name and selecting Add Table](./media/ch2/database17.png)

1. In the New Table name box on the top of VS Code, enter **person** as the table name. Then press Enter.

    ![A picture of entering person as the new table name](./media/ch2/database18.png)

1. We now have a simple create table script (person.sql) with a single column in our Database Project.

    ![A picture of the new person.sql script and the script contents](./media/ch2/database19.png)

1. To speed things along, we can use some pre-created code for the person table. Replace the code with the following:

    ```SQL
    CREATE TABLE [dbo].[person] (
        [person_id]        INT IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([person_id] ASC),
        [person_name]      NVARCHAR (200)      NOT NULL,
        [person_email]     NVARCHAR (200)      NOT NULL,
        [pet_preference]   NVARCHAR (100)      NOT NULL
    );
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the person.sql script](./media/ch2/database20.png)

1. If you didn't already, **save the file**.

1. Create another table by **right clicking** the project name again and select **Add Table**.

    ![A picture of right clicking the project name and selecting Add Table](./media/ch2/database21.png)

1. In the New Table name box on the top of the code space, enter **address** as the table name. Then press Enter.

    ![A picture of entering address as the new table name](./media/ch2/database22.png)

1. Replace the code in the editor with the following code:

    ```SQL
    CREATE TABLE [dbo].[address] (
        [address_id]        INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([address_id] ASC),
        [person_id]         INT            NOT NULL,
        [address]           NVARCHAR (200) NOT NULL,
        CONSTRAINT [FK_address_person] FOREIGN KEY ([person_id]) REFERENCES [dbo].[person] ([person_id])
    );
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the address.sql script](./media/ch2/database23.png)

1. If you didn't already, **save the file**.

1. Now that we have the two tables for the workshop, we can create a simple stored procedure. Right click on the project and select **Add Stored Procedure**.

    ![A picture of right clicking the project name and selecting Add Stored Procedure](./media/ch2/database24.png)

1. Name the new stored procedure get_person_by_pet and press enter

    ![A picture of entering get_person_by_pet as the new stored procedure name](./media/ch2/database25.png)

1. Replace the sample code with the following:

    ```SQL
    CREATE PROCEDURE dbo.get_person_by_pet
        @pet nvarchar(100)
    AS
    BEGIN
        select *
        from dbo.person
        where pet_preference = iif(NULLIF(@pet, '') IS NOT NULL,lower(@pet),lower(pet_preference));
    END;
    GO
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the get_person_by_pet.sql script](./media/ch2/database26.png)

1. If you didn't already, **save the file**.

### Create a table and stored procedures for the Todo application

1. Back in the Database Projects extension, right click the project name (devDB) and select **Add Table**

    ![A picture of right clicking the project name and selecting Add Table](./media/ch2/database17a.png)

1. In the New Table name box on the top of VS Code, enter **todo** as the table name. Then press Enter.

    ![A picture of entering todo as the new table name](./media/ch2/database17b.png)

1. We now have a simple create table script (todo.sql) with a single column in our Database Project.

    ![A picture of the new todo.sql script and the script contents](./media/ch2/database17c.png)

1. To speed things along, we can use some pre-created code for the todo table. Replace the code with the following:

    ```SQL
    CREATE TABLE [dbo].[todo]
    (
    	[id] [uniqueidentifier] NOT NULL,
    	[title] [nvarchar](1000) NOT NULL,
    	[completed] [bit] NOT NULL,
    	[owner_id] [varchar](128) NOT NULL,
    	[position] INT NULL
    ) 
    GO
    ALTER TABLE [dbo].[todo] ADD PRIMARY KEY NONCLUSTERED 
    (
    	[id] ASC
    )
    GO
    ALTER TABLE [dbo].[todo] ADD  DEFAULT (newid()) FOR [id]
    GO
    ALTER TABLE [dbo].[todo] ADD  DEFAULT ((0)) FOR [completed]
    GO
    ALTER TABLE [dbo].[todo] ADD  DEFAULT ('public') FOR [owner_id]
    GO
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the todo.sql script](./media/ch2/database17d.png)

1. If you didn't already, **save the file**.

1. Now that we have the table for the todo app, we can create stored procedures. Right click on the project and select **Add Stored Procedure**.

    ![A picture of right clicking the project name and selecting Add Stored Procedure](./media/ch2/database24.png)

1. Name the new stored procedure **insert_todo** and press enter

    ![A picture of entering insert_todo as the new stored procedure name](./media/ch2/database24a.png)

1. Replace the sample code with the following:

    ```SQL
    CREATE PROCEDURE dbo.insert_todo
        @title nvarchar(1000),
        @owner_id [varchar](128),
        @order int
    AS

    BEGIN

        insert into dbo.todo (title, owner_id, position)
        OUTPUT INSERTED.*
        values (@title, @owner_id, @order);

    END;
    GO
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the insert_todo.sql script](./media/ch2/database24b.png)

1. If you didn't already, **save the file**.

1. As before, right click on the project and select **Add Stored Procedure**.

    ![A picture of right clicking the project name and selecting Add Stored Procedure](./media/ch2/database24.png)

1. Name the new stored procedure **update_todo** and press enter

    ![A picture of entering update_todo as the new stored procedure name](./media/ch2/database24c.png)

1. Replace the sample code with the following:

    ```SQL
    CREATE PROCEDURE dbo.update_todo
        @id nvarchar(100),
        @title nvarchar(1000) = NULL,
        @owner_id [varchar](128),
        @completed bit = NULL,
        @order int = NULL
    AS

    BEGIN

        update dbo.todo 
           set title = ISNULL(@title,title),
               completed = ISNULL(@completed,completed),
               position = ISNULL(@order,position)
        OUTPUT INSERTED.*
         where id = @id
           and owner_id = @owner_id;

    END;
    GO
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the update_todo.sql script](./media/ch2/database24d.png)

    And again, for the final procedure, right click on the project and select **Add Stored Procedure**.

    ![A picture of right clicking the project name and selecting Add Stored Procedure](./media/ch2/database24.png)

1. If you didn't already, **save the file**.

1. Name the new stored procedure **delete_todo** and press enter

    ![A picture of entering delete_todo as the new stored procedure name](./media/ch2/database24e.png)

1. Replace the sample code with the following:

    ```SQL
    CREATE PROCEDURE dbo.delete_todo
        @id nvarchar(100),
        @owner_id [varchar](128)
    AS

    BEGIN

        delete from dbo.todo
        where id = @id
        and owner_id = @owner_id;

    END;
    GO
    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the delete_todo.sql script](./media/ch2/database24f.png)

1. If you didn't already, **save the file**.

### Create a post-deployment script

1. So that we have some data in the database when we deploy the objects, we can create a **Post-Deployment Script**. To create this script, right click on the project and select **Add Post-Deployment Script**.

    ![A picture of right clicking on the project and selecting Add Post-Deployment Script](./media/ch2/database24g.png)

1. Leave the default name of the script as **Script.PostDeployment1** and press enter to create it.

    ![A picture of leaveing the default name of the script as Script.PostDeployment1 and pressing enter to create it](./media/ch2/database24h.png)

1. Copy and paste the following text into the post-deployment script body in the editor:

    ```SQL
    -- This file contains SQL statements that will be executed after the build script.
    
    set identity_insert dbo.person on

    insert into dbo.person(person_id, person_name, person_email, pet_preference) values(1,'Bill','bill@contoso.com','Dogs');
    insert into dbo.person(person_id, person_name, person_email, pet_preference) values(2,'Frank', 'frank@contoso.com','Cats');
    insert into dbo.person(person_id, person_name, person_email, pet_preference) values(3,'Riley', 'Riley@contoso.com','Cats');

    set identity_insert dbo.person off

    set identity_insert dbo.address on

    insert into dbo.address (address_id, person_id, address) values (1, 1, 'Lincoln, MA');
    insert into dbo.address (address_id, person_id, address) values (2, 2, 'Baltimore, MD');

    set identity_insert dbo.address off

    insert into dbo.todo 
    (
        [id],
        [title],
        [completed],
        [owner_id],
        [position]
    ) 
    values
        ('00000000-0000-0000-0000-000000000001', N'Hello world', 0, 'public', 1),
        ('00000000-0000-0000-0000-000000000002', N'This is done', 1, 'public', 2),
        ('00000000-0000-0000-0000-000000000003', N'And this is not done (yet!)', 0, 'public', 4),
        ('00000000-0000-0000-0000-000000000004', N'This is a ☆☆☆☆☆ tool!', 0, 'public', 3),
        ('00000000-0000-0000-0000-000000000005', N'Add support for sorting', 1, 'public', 5)
    ;
    GO

    ```

    and **save the file**.

    ![A picture of the copy and pasted code into the Script.PostDeployment1.sql script](./media/ch2/database24i.png)

1. If you didn't already, **save the file**.

### Publish to the local database

1. To publish the database code to the local database, **right click** on the project and select **Publish**.

    ![A picture of right clicking the project name and selecting Publish](./media/ch2/database27.png)

1. Select "Publish to an existing Azure SQL logical server" on the first step

    ![A picture of selecting Publish to an existing Azure SQL logical server in the publish database modal flow](./media/ch2/database28.png)

1. Next, select "Don't use profile"

    ![A picture of selecting Don't use profile in the publish database modal flow](./media/ch2/database29.png)

1. Choose the **Local Database** connection you created previously for the connection profile step

    ![A picture of selecting the Local Database connection in the publish database modal flow](./media/ch2/database30.png)

1. Choose **devDB** as the database

    ![A picture of selecting the database named devDB in the publish database modal flow](./media/ch2/database31.png)

    **Note**: You may get a dialog box on the bottom right asking you "Would you like to save the settings in a profile (.publish.xml)?". Select **No**.

    ![A picture of selecting No for the save settings to a profile dialog box](./media/ch2/database31a.png)

1. And finally, for the action, choose **Publish**.

    ![A picture of selecting Publish in the publish database modal flow](./media/ch2/database32.png)

1. Once the dacpac is published into the database,

    ![A picture of the dacpac being published to the database successfully](./media/ch2/database33.png)

    you can go to the SQL Server Connection extension, reload the database objects by right clicking it and selecting refresh,

    ![A picture of reloading the database objects by right clicking it and selecting refresh in the SQL Server extension](./media/ch2/database34.png)

     then open the Tables and Programmability folders to see the deployed objects.

    ![A picture of the Tables and Programmability folders to and the deployed objects](./media/ch2/database35.png)

### Work with the database objects for testing

1. While still on the **SQL Server Connections extension**, right click the database profile name, Local Database, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the database profile name and selecting New Query](./media/ch2/database36.png)

1. Run the following code in the query sheet:

    ```SQL
    select * from person
    select p.person_name, a.address
    from person p, address a
    where p.person_id = a.person_id;
    go
    ```

1. You can also test out the stored procedure with the following code:

    ```SQL
    exec dbo.get_person_by_pet 'Dogs';
    ```

### Work with the database objects for the Todo Application

1. While still on the **SQL Server Connections extension**, right click the database profile name, Local Database, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the database profile name and selecting New Query](./media/ch2/database36.png)

1. Run the following code in the query sheet:

    ```SQL
    select * from dbo.todo;
    GO
    ```

1. You can also test out the stored procedures with the following code:

    ```SQL
    exec dbo.insert_todo @title = 'My Test Todo', @owner_id = '1001001', @order = 1;
    select * from dbo.todo;
    GO    
    ```

    Copy and paste the resulting ID from the result set into the area marked **COPIED ID FROM RESULT SET** and use it in the next stored procedure example:

    ```SQL
    exec dbo.update_todo @id = 'COPIED ID FROM RESULT SET', @title = 'zzzz', @owner_id = '1001001';
    select * from dbo.todo;
    GO    
    ```

    And again, copy and paste the resulting ID from the result set into the area marked **COPIED ID FROM RESULT SET** and use it in the next stored procedure example:

    ```SQL
    exec dbo.delete_todo @id = 'COPIED ID FROM RESULT SET', @owner_id = '1001001';
    select * from dbo.todo;
    GO    
    ```

## Continue to chapter 3

Click [here](./3-Data-API-builder.md) to continue to chapter 3, Data API Builder!