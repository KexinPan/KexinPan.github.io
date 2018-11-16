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

First, I tried the dot notation in LINQPad:
People.Where(p=>p.FullName.Contains("Lily")).Select(p=>p.FullName)
The output is : Lily Code (DbQueryString)

How to write this LINQ in C#, almost same, but if I want to use the table People, I need to use this table from database, so like in java, create an object (对象实例化): WorldWideContext db = new WorldWideContext();

Then use db.People to run the LINQ in C#: 
var name = db.People.Where(p => p.FullName.Contains(namePart)).Select(p => p.FullName);
string personName = name.ToString();
ViewBag.name = personName;

I totally can not remember how I get the value that user input in View, then run it in Action. 
Here is how I did it in the first time:
```
Request.QueryString["viewpagename"]; //please notice that here is [] not ()
```
The next part will chang to Chinese, write English is too slow and tired...

此时输出是没有任何结果的，只有一个文本显示框和一个按钮还有下面一行乱码，也不是乱码，主要是并没有规定当输入值不为空时才进行查找，所以此时输入框为空但仍然进行了ViewBag.name的值的传递，以往的解决办法都是立个flag, true or false进行输出，比如这样：

当没有输入任何值的时候，直接返回View，就是保持原样等待输入值
```
if (namePart == null)
 {
     ViewBag.name = false;
     return View();
}
```
当有值输入时再进行查找：
```
else
{
    ViewBag.name = true;
    var name = db.People.Where(p => p.FullName.Contains(namePart)).Select(p => p.FullName);
    return View(name.ToList()); //这里直接返回的是个list， 因为输入部分名字会返回多个值，所以这里不能用刚刚ViewBag.name的想法直接展示出来，等下在View中遍历一下输出值
 }
```
此时在View中修改@modle HW6Redo.Models.Person 为 @model IEnumerable<HW6Redo.Models.Person> 可楞是要用到foreach的原因吧









