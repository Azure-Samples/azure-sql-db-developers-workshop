![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Azure SQL Database Preview - JSON in the database

## JSON in the Azure SQL Database

Along with the the new JSON data type, transact-SQL supports various functions and operators that can be used to work with JSON documents in the Azure SQL database. The available functions are:

1. **ISJSON**: Tests whether a string contains valid JSON.
1. **JSON\_PATH\_EXISTS**: Tests whether a specified SQL/JSON path exists in the input JSON string.
1. **JSON\_MODIFY**: Updates the value of a property in a JSON string and returns the updated JSON string.
1. ANSI SQL JSON functions
    * **JSON\_VALUE**: Extracts a scalar value from a JSON string.
    * **JSON\_QUERY**: Extracts an object or an array from a JSON string.
1. ANSI SQL JSON constructors
    * **JSON\_OBJECT**: Constructs JSON object text from zero or more expressions.
    * **JSON\_ARRAY**: Constructs JSON array text from zero or more expressions.

The available operators are:

1. **FOR JSON**: Converts SQL Server data types to JSON types.
1. **OPENJSON**: Converts JSON text into a set of rows and columns.

## JSON in the Azure SQL Database workshop tasks

1. Start in the **SQL Server Connections extension** and right click the database profile name, **Azure Database**, and select **New Query**. This will bring up a new query sheet.

    ![A picture of right clicking the Azure Database profile name and selecting New Query](./media/ch7/json1.png)

1. In the empty query sheet, enter the following SQL code. This statement creates the **Orders** table with the new **JSON** data type.

    ```SQL
    CREATE TABLE dbo.Orders (
        order_id int NOT NULL IDENTITY,
        order_info JSON NOT NULL
    );
    ```

1. Next, issue the following insert statements. They will be inserting JSON into the JSON type columns:

    ```SQL
    INSERT INTO dbo.Orders (order_info)
    VALUES ('
    {
        "OrderNumber":"S043659",
        "Date":"2022-05-24T08:01:00",
        "AccountNumber":"AW29825",
        "Price":59.99,
        "Quantity":1,
        "Address": {"Line1":"1124 NE 10th PL", "ZipCode":98074, "Country":"USA"}
    }'), ('
    {
        "OrderNumber": "S043661",
        "Date":"2022-05-20T12:20:00",
        "AccountNumber":"AW73565",
        "Price":24.99,
        "Quantity":3,
        "Address": {"Line1":"2145 145th SE Ave", "ZipCode":98052, "Country":"USA"}
    }');
    ```

    Issue this select statement to see the inserted rows

    ```SQL
    SELECT * FROM dbo.Orders;
    ```

    ![A picture of the inserted rows with the JSON type column](./media/ch7/json2.png)

1. Click the order_info data cell 

    ![A picture of clicking on the a data cell in the JSON type column](./media/ch7/json3.png)

    to see the JSON in the column in a new window.

    ![A picture of the JSON in the JSON type column](./media/ch7/json4.png)

1. Using [JSON_VALUE](https://learn.microsoft.com/sql/t-sql/functions/json-value-transact-sql) and [JSON_QUERY](https://learn.microsoft.com/sql/t-sql/functions/json-query-transact-sql), JSON stored in the database can be extracted as a relational result set. Using the query sheet, issue the following command:

    ```SQL
    SELECT o.order_id
         , JSON_VALUE(o.order_info, '$.AccountNumber') AS account_number
         , JSON_QUERY(o.order_info, '$.Address') AS address_info
      FROM dbo.Orders as o;
    ```



    ```SQL
SELECT SUM(CAST(JSON_VALUE(o.order_info, '$.Price') as float)) AS total_price
     , SUM(CAST(JSON_VALUE(o.order_info, '$.Quantity') as int)) AS total_orders
  FROM dbo.Orders as o;