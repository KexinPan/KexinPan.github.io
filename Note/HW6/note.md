### How to restore a database:
Get the BAK file first, there is a [link](https://www.howtogeek.com/50354/restoring-a-sql-database-backup-using-sql-server-management-studio/) to tell you how to restore database. Your server name is "localhost"

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
### How to add database to the mvc and use vode first

Here is a [link](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database)




