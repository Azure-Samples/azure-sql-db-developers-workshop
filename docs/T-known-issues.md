![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Known issues and troubleshooting

## SQL server in docker is not starting as the container was marked as shutting down

If the SQL Server in docker instance is not starting after repeated attempts to start with the Docker Extension in codespace, stop and restart the code space. To stop the codespace, press F1 and type stop into the text field. Find and select **Codespaces: Stop Current Codespace**. Once stopped, you can click the Green Restart Codespace button.

## codespace terminal is not responding

If the codespace terminal is not responding to typing, delete the session and re-create a new one. Use the kill terminal icon to kill the session and create a new one.

![A picture of using the kill terminal icon to kill the session](./media/cht/trouble1a.png)

## codespace pauses/shuts down

If your codespace shuts down, you can restart it by refreshing the page. After the codespace comes back up, remember to restart the local database in the docker extension by right clicking on it and selecting start.

![A picture of restarting the database using the docker extension in codespace](./media/cht/trouble1.png)

another option to do the same, if you prefer a command line approach, is to run, from the terminal, the following command:

```bash
sqlcmd start
```

## Data API builder (DAB) starts on port 8080

If you start dab (dab start), and it comes up on port 8080, you can change it back to 5000 with the following steps.

1. Stop dab with a crtl-C

2. Run the following code in the terminal

    ```BASH
    echo 'export ASPNETCORE_HTTP_PORTS=5000' >> ~/.bashrc
    ```

    followed by running this code in the terminal to uptake the new change

    ```BASH
    . ~/.bashrc
    ```

3. Start up dab again (dab start)

## Going to the Banana Cake Pop URL results in an error

If you append /graphql at the end of the URL in chapter 3 you will get the following resulting URL

```code
https://upgraded-telegram-rvwpqqqj7v2x7gg-5000.app.github.dev:5000/graphql/
```

and the page will not load.

You can resolve this by appending /graphql/ at the end of the URL or modify the wrong URL that results in an error to be similar to the following:

```code
https://upgraded-telegram-rvwpqqqj7v2x7gg-5000.app.github.dev/graphql/
```

## Testing connection profile message on the bottom right of the codespace and an unexpected error message on reload

If you see the testing connection profile message on the bottom right of the codespace constantly running 

![A picture of the testing connection profile message on the bottom right of the codespace](./media/cht/trouble2.png)

and with a page reload, an error message stating:

An unexpected error occurred that requires a reload of this page.
The workbench failed to connect to the server (Error: )

![A picture of an error message stating a reload of the page is required](./media/cht/trouble3.png)

go back to the main repository page and click on the green code button

![A picture of clicking on the green code button on the main repository page](./media/cht/trouble4.png)

Find the active codespace you are using and click the pop out menu. Then select **Stop codespace**.

![A picture of clicking the pop out menu next to the active code space then selecting Stop codespace](./media/cht/trouble5.png)

Once it is stopped, you can click on the codespace name and open it in a new tab. This will start the codespace back up and the error will be resolved.

![A picture of clicking on the codespace name and opening it in a new tab](./media/cht/trouble6.png)
