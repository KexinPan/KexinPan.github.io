## Apply to Azure

The summary of the process looks like:
1. build a resource group
2. build a sql database and a sql server
3. Add a firewall rule to allow your IP address to get the server
4. connect to the SSMS
5. connect to the Visual Studio, use the Azure in Server Exployer
⋅⋅⋅In visual studio, open the Server Exploer, right click the database name, open in sql server object exploer
⋅⋅⋅conect Azure: use Admin name and password
6. run the UP sricpt
7. get the coonection string from Azure, then replace the local one in Web.config by this one, when you push them to repo, please replace them in some others
8. create an App on Azure: app services-> web app
9. in application setting, add a connetion string: choose sql server,name is the context name and value is the connect string in Wen.config
10. Deploy the app to Azure: Build-> Publish->app aervices->select existing->publish->choose one and ok
