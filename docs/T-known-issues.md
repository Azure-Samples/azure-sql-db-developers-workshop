![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Known issues and troubleshooting

## codespace terminal is not responding

If the codespace terminal is not responding to typing, delete the session and re-create a new one. Use the kill terminal icon to kill the session and create a new one.

![A picture of using the kill terminal icon to kill the session](./media/cht/trouble1a.png)

## codespace pauses/shuts down

If your codespace shuts down, you can restart it by refreshing the page. After the codespace comes back up, remember to restart the local database in the docker extension by right clicking on it and selecting start.

![A picture of restarting the database using the docker extension in codespace](./media/cht/trouble1.png)

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
