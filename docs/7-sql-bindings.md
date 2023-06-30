![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Implement Change Data Streams with Azure SQL Database, Change Tracking, and Azure SQL Bindings

Enable change tracking
In the SQL Server extension, right click on the connection profile and select New Query.
Image Screenshot 2023 05 12 095051
This will open up a new query sheet where SQL commands can be run. The table needs change tracking enabled for the SQL bindings trigger to work correctly.
The first command to be run will enable change tracking in the database.
ALTER DATABASE [devDB]
SET CHANGE_TRACKING = ON;
GO
then enable change tracking on the customer table:
ALTER TABLE [dbo].[customer] ENABLE CHANGE_TRACKING;
GO
In this section, you created a SQL Database project, created a database right in your codespace with a single command, and published objects from the SQL Database project directly into the locally running database. The next section will create an Azure Function in which to house the SQL Bindings Trigger to watch for data changes.
Create an Azure Function
Back in the terminal at the bottom of the page,
Back to the terminal

You may have to click the terminal tab on the window bar on the bottom of the page. When you deployed the SQL Database Project, it may have moved from the terminal window to the Output window.
Image Screenshot 2023 05 12 100117
issue the following command to change the directory back to the top level of this project:
cd /workspaces/codespace-for-DB-Devs
Image Screenshot 2023 05 12 100517
then, issue the following command to start the function creation process:
func init
Upon issuing that command, you will be presented with a choice of frameworks for this function to use.
Image Screenshot 2023 05 12 100727
The framework/runtime for this project is dotnet so enter 1, then press enter.
Image Screenshot 2023 05 12 100604
The next option is to choose a language.
Image Screenshot 2023 05 12 100959
C# is used for this project so again, enter 1, then press enter.
Image Screenshot 2023 05 12 101054
When this process is finished, click the File Explorer extension to see the new files that were created for you.
Image Screenshot 2023 05 12 101239
Adding necessary libraries to the project
Next, we need to add some package references to the project (for SQL Bindings and Azure Event Hub). The following commands will add these references to the codespace-for-DB-Devs.csproj file.
Run the following commands in the terminal:
dotnet add package Microsoft.Azure.WebJobs.Extensions.Sql --prerelease
Image Screenshot 2023 05 12 101626

dotnet add package Azure.Messaging.EventHubs
dotnet add package Microsoft.Azure.WebJobs.Extensions.EventHubs
Image Screenshot 2023 05 12 101700

Create the customer class object
We are going to create a customer class object file. To create a new file in codespaces, right click below the files in the file explorer extension and select New File.

Image Screenshot 2023 05 12 102750

Name this file Customer.cs and press enter.

Image Screenshot 2023 05 12 102912

If the new file has not opened up for you in codespaces, select this file by right clicking on it. Copy and paste the following code into the Customer.cs file to create the customer class object.

namespace Company.Function;
public class Customer
{
    public int customer_id { get; set; }
    public string customer_name { get; set; }
    public string customer_email { get; set; }
    public string customer_address { get; set; }
}
Image Screenshot 2023 05 12 130007
Image Screenshot 2023 05 12 103252
and SAVE the file.
Create the SQL trigger function
The next step is to create an Azure Function. Start by pressing F1 or Shift-Ctrl-P to bring up the command palette.
Image Screenshot 2023 05 12 105414
Enter “create function” into the text field and then select Azure Functions: Create Function.
Image Screenshot 2023 05 12 105520
A dialog box will appear in the center of the screen asking to “Initialize project for use with VS Code?”. Click Yes in the dialog box.
Image Screenshot 2023 05 12 105712
In the following dialog box, Select “HTTP Trigger” as the function template.
Image Screenshot 2023 05 12 110427
Now, on step 2, name the Function changeDataStreams in the next dialog box, and then press Enter.
Image Screenshot 2023 05 12 110556
Step 3 is for the function namespace. Accept the default namespace of Company.Function, and then press Enter.
Image Screenshot 2023 05 12 110711
In step four, select “Anonymous” for AccessRights.
Image Screenshot 2023 05 12 110726
Looking at the file explorer, there will be a new file called changeDataStream.cs which should also automatically open up in codespaces for you.
Image Screenshot 2023 05 12 110941
Adding the SQL Bindings code
If the file is not already open, open the file by right clicking on it. Replace the code in the file with the following:
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using System.Threading.Tasks;
using System.Text.Json;
namespace Company.Function;
public static class streamCustomers
{
    [FunctionName("changeDataStream")]
    public static async Task RunAsync(
        [SqlTrigger("[dbo].[customer]", "SqlConnectionString")]
            IReadOnlyList<SqlChange<Customer>> changes,
        ILogger logger)
   {
      foreach (SqlChange<Customer> change in changes)
      {
          var customer = JsonSerializer.Serialize(change.Item);
          var message = $"{change.Operation} {customer}";
          logger.LogInformation(message);
        }
    }
}
and SAVE the file. This code uses the SQL trigger binding to watch the customer table for changes. When it sees a change, it will fire and so something for each change. Here, in this code, we are just logging the data change to the terminal.
Now that the function code is done, we need to provide it a value for SqlConnectionString. This variable can be places in the local.settings.json file and contain the connect string for our locally running database.
Image Screenshot 2023 05 12 112130
Open the local.settings.json file and add the following line just below the “Values”: { section:
"SqlConnectionString": "Server=localhost,1433;Database=devDB;User ID=vscode;Password=XXXXX;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;",
Image Screenshot 2023 05 12 112130 8211 Copy

and be sure to replace the XXXXX with the password of the database that was created before. If you forgot the password, run the following in the terminal to find it again:

sqlcmd config connection-strings
And remember to SAVE the file when done.

Test the trigger
Time to test the trigger and see if it catches changes to the customer table in the database. At the terminal run the following command to start the Azure Function:

func host start
Image Screenshot 2023 05 12 112618

and once the function is started, open a new query sheet for the devDB database

Image Screenshot 2023 05 12 095051

and issue the following SQL insert statement:

insert into dbo.customer values(N'Bill', N'bill@computer.commm', N'Anytown, Anycity 12345');
You should see the following in the terminal window indicating the trigger binding did see the change:
[2023-05-11T19:18:03.124Z] Executing 'changeDataStream' (Reason='New change detected on table '[dbo].[customer]' at 2023-05-11T19:18:03.0902305Z.', Id=541ff09e-54ac-48e8-8d17-bbcc9a451432)
[2023-05-11T19:18:03.176Z] Insert{"customer_id":1,"customer_name":"Bill","customer_email":"bill@computer.commm","customer_address":"Anytown, Anycity 12345"}
[2023-05-11T19:18:03.195Z] Executed 'changeDataStream' (Succeeded, Id=541ff09e-54ac-48e8-8d17-bbcc9a451432, Duration=93ms)
