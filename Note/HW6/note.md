### How to restore a database:
Get the BAK file first, there is a [link](https://www.howtogeek.com/50354/restoring-a-sql-database-backup-using-sql-server-management-studio/) to tell you how to restore database. Your server name is "localhost"

### From an existing database, reverse engineering the model

Here is a [link](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database)
The general process:
1. In data connection, add the connection first.
2. Change the route in Global.asax(the content below)

IF: This database uses a data type (DbGeography) for locations that is not installed by default in an MVC app. You'll need to use Nuget to add Microsoft.SqlServer.Types to your project. In addition you'll need to add these lines to your Global.asax.cs file, i.e. the first two lines in:
```
protected void Application_Start()
{
    // For Spatial types, i.e. DbGeography
    SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
    // This next line is a fix that came from: https://stackoverflow.com/questions/13174197/microsoft-sqlserver-types-version-10-or-higher-could-not-be-found-on-azure/40166192#40166192
    SqlProviderServices.SqlServerTypesAssemblyName = typeof(SqlGeography).Assembly.FullName;

    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
}
```
3. In Model, add new item: Data==> ADO.NET Entity Data Model(Name: xxxContext)
    notice the requirements, for this assignment, you don't have to add .archive table and views. Move the context file to DAL folder
### Add database in linqpad
1. Add connection : Entity Framwork (DbContext)
2. Browse: project/bin/projectname.dll
### Code(People Search: show a button below and when you click it, you will into another page to see more details)
General ideas: 
1. add a link in the button, when click it, it can into another page to show more details.
2. through the input to find name, through the name to find id, through the id to show more details
3. when click the name button, the button back with an id to another Detail action
4. make a flag, if it has customer, continue more details






