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
```
People.Where(p=>p.FullName.Contains("Lily")).Select(p=>p.FullName)
The output is : Lily Code (DbQueryString)
```
How to write this LINQ in C#, almost same, but if I want to use the table People, I need to use this table from database, so like in java, create an object (对象实例化): 
```
WorldWideContext db = new WorldWideContext();
```
Then use db.People to run the LINQ in C#: 
```
var name = db.People.Where(p => p.FullName.Contains(namePart)).Select(p => p.FullName);
```
//这里是具体的找到并返回了FullName,其实可以直接返回Person对象，然后等下在View中可以获取到对应的ID以便于更多信息的展示，就是这样： 

```
People.Where(p=>p.FullName.Contains(namePart))
string personName = name.ToString();
ViewBag.name = personName;
```
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
    return View(name.ToList()); //这里直接返回的是个list， 因为输入部分名字会返回多个值，所以这里不能用刚刚ViewBag.name的想法直接展示出来，等下在View中遍历一下输出值,在view中输出值的时候就是 Model，比如想看传入的值是否为0， Modle.Count(), 可楞是因为你传入的是 Person类型吧
 }
```
此时在View中修改@modle HW6Redo.Models.Person 为 @model IEnumerable<HW6Redo.Models.Person> 可楞是要用到foreach的原因吧

关于foreach的使用(个人理解如果在Razor里面则不需要再加@)：
```
foreach(var p in Model){}
```
关于本来限制输入值结果成了内容在里面的原因，请一定要在new前面加上“”用来表示object value为空，然后这样required才会成html value
```
 @Html.TextBox("search", "",new {required = "required" })
```
 
 以上就可以显示搜索后的名字按钮了（真是不容易啊），使用给按钮加连接来跳转到另一个显示更多信息的页面，这里的按钮加的连接是hrf="ActionName/@p.PersonID".
 
 这一块具体代码如下：
 ```
 @model IEnumerable<HW6Redo.Models.Person>


@using (Html.BeginForm("Search", "Home", FormMethod.Get))
{

        @Html.TextBox("search", "",new {required = "required" })
        <button type="submit">Search</button>

}

@if (ViewBag.message)
{

    if (Model.Count() != 0)
    {
        foreach (var p in Model)
        {
            <button type="button" href="Details/@p.PersonID">@p.FullName</button>
            //来更正了，button里没有加链接的不好意思
            //只能用这种方法使链接角色为button：<a href="Details/@p.PersonID" role="button">@p.FullName</a>
        }

    }
    else
    {
        <p>There is no person matches.</p>
    }
}
 ```
下面开始展示更多信息，先写Action: Details:
发现通过id获取到person object之后返回View,但是此时就开始迷用哪个model来设置View，因为这个View之后也要包含其他Model比如customer或者Order的内容，根据上一次的经验是要写一个ViewModel包含这些内容

那先写ViewModel:
```
 public class Information
    {
        public Person Person { get; set; }

        public Customer Customer { get; set; }

        public List<InvoiceLine> InvoiceLine { get; set; }
    }
```
挺简单的，需要啥加进去啥就行了

然后回到Action的编写：
请注意传入id值时记得加?,因为有可能是空值（当然你编写正确了按理来说不可能有空值，有个名词叫严谨）
就像这样： public ActionResult Details(int? id)
接下来的步骤比第一次做的时候清晰多了，有了ViewModel,使用ViewModel add 一个 View， 然后在Action里面需要传入一样的类型，所以实例化一个ViewModel：
```
Information information = new Information();
```
之后的根据id找人啊之类的就用ViewModel来完成就好了， 这里的ViewModel是information
```
information.Person=db.People.Find(id);
```
最后传值到View时也是：return View(information);

到这里想检验一下写的对不对，我在View里写了两个这个：
@Html.Display(Model.Person.FullName)
@Html.DisplayFor(m=>m.Person.FullName)
因为我不懂他们有什么区别，想看运行下结果是不是一样，当我运行的时候果不其然我出错了，虽然目前没找出来，决定先写着Customer的代码找出原因了再回来补
（好了我现在知道了，我不能运行Details的View,我要运行Search并传值进入Details，然后现在运行这两个第一个没有任何反应目前还是不知道什么我以为效果是一样的）

此处关于老师的特殊要求：邮箱地址加入邮箱特殊链接：
[有关的资料](https://www.reddit.com/r/csharp/comments/6282ww/htmldisplayfor_with_mailto/)
```
@Html.Label("Email: ")

<a href="mailto:no-one@snai1mai1.com?subject=free">@Html.DisplayFor(m => m.Person.EmailAddress)</a>
```
或者按照第一次的写法更加简单，还可以变通成其他的格式：
```
<a href="mailto:@(Model.Person.EmailAddress)">@Html.DisplayFor(modelItem => modelItem.Person.EmailAddress)</a>
```
接着回去写Customer2的信息，要求大概是:
Company Profile: Company;Phone;Fax;Website;Member Since
Purchase History: Orers Total; Gross Sales; Gross Profit
Item Purchased: List Top 10 of the profit item purchased: StockItemID; Description; 

关于Company Profile里面的内容我觉得直接在View里面获取输出就可以了，比如我的想法是下面这样:
在LINQPad里面：
```
People.Where(p=>p.PersonID==1001).Select(p=>p.Customers2)
//运行出来的结果是一个ID为1001的Customer的HashSet
```
然后我觉得完美啊，就在View里面这样写：
```
@Html.Label("Company : ")

@Html.DisplayFor(m => m.Person.Customers2.ToList().Select(c=>c.CustomerName))
//我觉得相当nice啊，怎么看都没有什么错误吧
//但是运行时依然报错，报的是啥我也没太看懂，大概就是这种样式不适合传递这样的参数
```
然后我只好翻了翻第一次成功时的做法：
```
//他先在Action里获得了CustomerID，然后用CustomerID去获得其他信息
//我忽略一个问题，就是Customer的返回类型是HashSet，然后HashSet要像Array一样通过下标取值第几个才能获得相应内容
int customerID = vm.Person.Customers2.First().CustomerID;
vm.Customer = database.Customers.Find(customerID);//真特么聪明
//在View里面
@Html.DisplayFor(c=>c.Customer.CustomerName)
//当写到网页的时候，老师要求以链接的形式展示出来，这个是真的没找到，但是按照上面第一次的写法变通一下就好了：
<a href="@(Model.Customer.WebsiteURL)">@Html.DispalyFor(c=>c.Customer.CustomerName)</a>
```
对于获取Profit的时候我用了两个foreach循环，但由于InvoiceLines中没有Enumerable所以无法继续使用foreach，参考一眼第一次做的时候还是用的dot notation获取到的所有的结果,请注意在SelectMany里面是要用Lymta Function的：
```
 ViewBag.grossSale=information.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).Sum(i => i.ExtendedPrice);
 ViewBag.grossProfit = information.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).Sum(i => i.LineProfit);
```
然后在View中使用ViewBag开始展示：
```
@Html.Label("Gross Sales : ")

    <p>@ViewBag.grossSale</p>

    @Html.Label("Gross Profit: ")

    <p>@ViewBag.grossProfit</p>
```

最后一个要求展示以Profit TOP 10的订单：我的一开始的思维是在View中foreach循环每个Order,结果后来发现 stockItemID, description啥的都是在InvoiceLines里面的，这就给了我一个hint, 说明我们的information要返回的是invoiceLines(这让我不禁开始担忧如果我的数据库无法载入LINQPad该怎么办)
在Action中使用 dot notation获取到InvoiceLines：
```
information.InvoiceLine = information.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).OrderByDescending(i => i.LineProfit).Take(10).ToList();

//一开始忘了加Tolist导致报错，看看报错原因到时候应该明白加上
```
然后在View里面：
```
 @foreach (var x in Model.InvoiceLine)
    {
        @Html.DisplayFor(i => x.StockItemID)
        @Html.DisplayFor(i => x.Description)
        @Html.DisplayFor(i => x.LineProfit)
        @Html.DisplayFor(i => x.Person.FullName)
    }
```
整个就算重新又做了一遍，虽然过程惨不忍睹，第一次该打但是没打的笔记都记下来了，遇到的问题也记下来了，最后关于照片的问题，你还没有放照片，这里有一个[网站](https://via.placeholder.com/150x200)，或者你也可以直接搜：photo placeholder，我觉得用一个img tag放个链接进去就得了，就像这样：
```
<div>
        <img src="https://via.placeholder.com/150x200"/>
    </div>
```


2
