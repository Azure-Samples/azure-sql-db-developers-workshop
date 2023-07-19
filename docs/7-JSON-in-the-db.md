![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Azure SQL Database Preview - JSON in the database

## JSON in the Azure SQL Database

Transact-SQL supports various functions and operators that can be used to work with JSON documents in the Azure SQL database. The available functions are:

1. ISJSON
1. JSON\_PATH\_EXISTS
1. JSON\_MODIFY
1. ANSI SQL JSON functions - JSON\_VALUE & JSON\_QUERY
1. ANSI SQL JSON constructors - JSON\_OBJECT & JSON\_ARRAY

The available operators are:

1. FOR JSON
1. OPENJSON

## JSON in the Azure SQL Database workshop tasks

1. Start in the **SQL Server Connections extension** and right click the database profile name,**Azure Database**, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the Azure Database profile name and selecting New Query](./media/ch7/json1.png)

1. In the empty query sheet, enter the following SQL code:
