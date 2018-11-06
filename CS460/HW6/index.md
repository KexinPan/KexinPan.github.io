## Homework 6

For this task, we're going to create a MVC project from an existing database using Entity Framework and LINQ queries. For the final effect of the project, we can search a person by part of the name from the database and show his or her information. For the person who is customer, we need to show more details from tables which connect to People table.

## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW6_1819.html)
* [Code Repository](https://github.com/KexinPan/CS460/tree/master/HW6)
* https://github.com/KexinPan/CS460.git

### Setup

This part is not hard, but I spent some time on finding my local sever name when I try to restore the database in SQL Server Management Studio. I searched online and finally in a Youtube video, I saw I need to download SQL Server first. After that, the local server name will appear in the final page.

### Coding (Feature1)

To be honest, this part is so hard, I have no idea where should I start. I was condused how can I use my database first, then I realize the last time we used DbContext. So I did almost same thing this time.

```
private EFWideWorldImportersContext database = new EFWideWorldImportersContext();
```
For the search part, my idea is write a LINQ query which can use the part of the name find the person, then it appears as a button with link which the link can help to see more information of this person. And I should use foreach to list all the person that match the part of the name.

It's easy to write the LINQ query, but it's hard to transfered to the mvc project. I failed several times, then on the class, I was told I should make the result ToList before I return the View. 

Here is the code of the action look like:
```
       public ActionResult Search(String namePart)
        {
            namePart = Request.QueryString["search"];

            if (namePart == null)
            {
                ViewBag.message = false;
                return View();
            }
            else
            {
                ViewBag.message = true;
                var peopleMatch = database.People.Where(p => p.FullName.Contains(namePart));
                return View(peopleMatch.ToList());
            }
        }
```
And in the View, if I want to use foreach, I need to use the IEnumerable interface.

Here is the code of the view look like:

```
@model IEnumerable<HW6.Models.Person>

@{
    ViewBag.Title = "Client Search";
}

<img src="~/Content/Image/strip.jpg" class="img-responsive" alt="Responsive image">

<h2>Client Search</h2>
@using (Html.BeginForm("Search", "Home", FormMethod.Get, new { @class = "form-inline"}))
{
    <div class="center-block">
            @Html.TextBox("search", "", new { @class = "form-control", @placeholder = "Search by client name", required = "required", @style= "width:600px" })
            <button type="submit" class="btn btn-default" style="background-color:#ffe6f7;">Search</button>     
    </div>
}

@if (ViewBag.message)
{
    if (Model.Count() == 0)
    {
        <h4>I'm sorry, your search returns no results.</h4>
    }
    else
    {
        <br/>
        <div>
            <h4>Names matching your search:</h4>
        </div>
        foreach (var person in Model)
        {
        <div>
            <a href="Information/@person.PersonID" class="btn btn-primary btn-lg active" role="button" style="background-color:#ffccef; width:500px">@person.FullName</a>
        </div>
        }
    }
}
```

### Coding (Feature2)

After finish the feature1, this part feels easier with ViewModel. The LINQ syntax is easy to write but when it runs in the LINQPad, it was so slow, so I just copied them to the mvc project if there is no mistake.

Here is the code of the action look like:

```
public ActionResult Information(int? id)
        {
            ViewModel vm = new ViewModel();
            vm.Person = database.People.Find(id);

            if (vm.Person.Customers2.Count() > 0)
            {
                ViewBag.customer2 = true;
                int customerID = vm.Person.Customers2.First().CustomerID;
                vm.Customer = database.Customers.Find(customerID);
                ViewBag.grosssales = vm.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).Sum(i => i.ExtendedPrice);
                ViewBag.grossprofit = vm.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).Sum(i => i.LineProfit);
                vm.InvoiceLine = vm.Customer.Orders.SelectMany(i => i.Invoices).SelectMany(i => i.InvoiceLines).OrderByDescending(i => i.LineProfit).Take(10).ToList();
            }
            else
            {
                ViewBag.customer2 = false;
            }
            return View(vm);
        }
```
When I try to show the top 10 orders from the InvoiceLine, I met some problems about the foreach, it can only use with IEnumerable interface, so I go back to the ViewModel and change the type of InvoiceLine to List.

Here is the code of the ViewModel:

```
public class ViewModel
    {
        public Person Person { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceLine> InvoiceLine { get; set; }
    }
```

Here is the part of the code of View:

```
<div class="row">
    <div class="col-md-7" style="background:#ffcccc;width:500px;height:150px;margin-top:20px">
        <h4>Purchase History Summary</h4>
        <hr />
        <table>
            <tr>
                <td class="col-md-2">@Html.Label("Orders:")</td>
                <td class="col-md-2">@Html.DisplayFor(modelItem => modelItem.Customer.Orders.Count)</td>
            </tr>
            <tr>
                <td class="col-md-2">@Html.Label("Gross Sales:")</td>
                <td class="col-md-2">$@ViewBag.grosssales</td>
            </tr>
            <tr>
                <td class="col-md-2">@Html.Label("Gross Profits:")</td>
                <td class="col-md-2">$@ViewBag.grossprofit</td>
            </tr>
        </table>
    </div>
</div>

<div class="row">
    <div class="col-md-7" style="background:#ffcccc;width:500px;height:auto;margin-top:20px">
        <h4>Item Purchased <span style="color:#662000;font-size:12px">(10 Highest by Profit)</span></h4>
        <hr />
        <table>
            <tr>
                <th>@Html.Label("StackItemID")</th>
                <th>@Html.Label("Description")</th>
                <th>@Html.Label("LineProfit")</th>
                <th>@Html.Label("SalesPerson")</th>
            </tr>
            @foreach (var item in Model.InvoiceLine)
            {
                <tr>
                    <td>@Html.DisplayFor(m=>item.StockItemID)</td>
                    <td>@Html.DisplayFor(m => item.Description)</td>
                    <td>$@Html.DisplayFor(m => item.LineProfit)</td>
                    <td>@Html.DisplayFor(m => item.Invoice.Person.FullName)</td>
                </tr>
            }
        </table>
    </div>
</div>
```

### Test

At first, I made a video for the assignment. But the video is too large to put it in the repository. So I changed to add some screenshots to explain my project.

Here are some screenshots:

The main page:

![mainPage](HW5/MainPage.jpg)

The original list:

![originalList](HW5/OriginalList.jpg)

Create new requests:

![create new requests](HW5/CreateRequest.jpg)

Submit the request without input:

![without input](HW5/WithoutInput.jpg)

Submit the request with incorrect input:

![incorrct input](HW5/IncorrectInput.jpg)

When complete the request, then redirect to a thanksPage:

![thnksPage](HW5/ThanksPage.jpg)

See the new request in the list:

![see new request](HW5/AddNewRequest.jpg)

