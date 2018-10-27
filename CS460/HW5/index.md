## Homework 5

For this task, we're going to create a MVC project with a simple database by using asp.net, the project seems not hard after lecture on class. We need to build a page for tenants to create the request and show all the requests in the database.

## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW5_1819.html)
* [Code Repository](https://github.com/KexinPan/CS460/tree/master/HW5)
* https://github.com/KexinPan/CS460.git

### Coding

First we need to create a Model which includes all the contents that a request form need from tennant. I struggled in the format of the phone number for a long time, because I want to make the phone number can transfer to the format of xxx-xxx-xxxx automaticallt when user just input numbers. I tried many ways but I filed, I use the regular expression to limit the input finally.

The part of the code of the model look like:

```
        [Required, Display(Name = "First Name:")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Please Input Your Legal Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name:")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Please Input Your Legal Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Phone Number:")]
        [RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage = "Plese Input Phone Number Like: xxx-xxx-xxxx")]
        public string PhoneNumber { get; set; }

```
The other parts of the code are fluent. Create a new database in App_Data folder and then add the connection string of the database to Web.config file. Then create a new folder named DAL(Data Access Layer). Add a database contect which inherit from DbContext, so I need to insatll entity framework. Use two queries drop and create table.

Here are some part of code of the context look like:

```
 public class RequestContext: DbContext
        {
        // passing the name of database to Dbcontext structure
        public RequestContext() : base("name=RequestList")
        {
        }
        // get access to the table
        public virtual DbSet<RequestForm> request { get; set; }
```

When the user complete the request, I redirect them to a thanks page instead of return the list of requests.

Here are some part of code of the Create action look like:

```
       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,ApartmentName,UnitNumber,Description,Permission")]          RequestForm requestForm)
        {
            if (ModelState.IsValid)
            {
                db.request.Add(requestForm);
                db.SaveChanges();
                return RedirectToAction("ThanksPage");
            }
            return View(requestForm);
        }
```
The lambda function can help me back to the information easily.

Here are some part of code of Create View look like:

```
@model HW5.Models.RequestForm
@{
    ViewBag.Title = "Create";
}
<h2>Campus Apartments</h2>
@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    <div class="form-horizontal">
        <h4>Teanant Request Form</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.FirstName, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.TextBoxFor(model => model.FirstName, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.FirstName, "", new { @class = "text-danger" })
            </div>
        </div>
```

### Test

After debugging several times, the code works well and the effect looks like the video shows:

The main page:

![mainPage](Code/mainPage.jpg)

When run mile convert with empty value:

![mileConvert-emptyValue](Code/mileConvert-emptyValue.jpg)

When run mile convert with unappropriate input:

![mileConvert-wrongValue](Code/mileConvert-wrongValue.jpg)

When run mile convert with correct input:

![mileConvert](Code/mileConvert.jpg)

When run color mixer with empty value:

![colorMixer-emptyValue](Code/colorMixer-emptyValue.jpg)

When run color mixer with unappropriate input:

![colorMixer-wrongValue](Code/colorMixer-wrongValue.jpg)

When run color mixer with correct input:

![colorMixer](Code/colorMixer.jpg)
