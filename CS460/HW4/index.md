## Homework 3

For this task, we're going to complete the task of creating a C# implementation of a previously written Java application. The goal of this project is to write the sample C# application to demonstrate the knowledge of the C# language and development tools.

From this project, I found java and C# are similar, there are some different syntax and you need to replace the common variable to another.


## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW4.html)
* [Code Repository](https://github.com/KexinPan/CS460/tree/master/HW4)
* https://github.com/KexinPan/CS460.git

### Enviroment Setup

#### Setting Up MVC Project

This is the first time I create .net project, I followed the instruction on class and it becomes pretty easier than I struggled by myself.

For the .gitignore file I used the last time in the homework 3, but there are still a lot of files and they seem necessary for this project. So I leave them and push to the remote.

#### Coding

The mileConvert is easy when I figured out the relationship between Controller and View, it's super smart by match the name to find the action and return corresponding ViewPage.

I added an action called MileConvert in HomeController and add a view for it.

I don't think I should put the complet code in the portfolio, so I select some code and paste here.

The C# Code for this action look like:

```
 [HttpGet]
        public ActionResult MileConvert()
        {
            /// get the value of miles in user input
            string mileValue = Request.QueryString["miles"];
            /// get the type of unit that usee choose
            string unit = Request.QueryString["unit"];
            /// set the result as a double value
            double value = Convert.ToDouble(mileValue);
            if (mileValue != null)
            {
                string message = "non";
                if (unit.Equals("millimeters"))
                {
                    message = value + " miles is equal to " + value * 1609344 + " millimeters";
                }
                if (unit.Equals("centimeters"))
                {
                    message = value + " miles is equal to " + value * 160934.4 + " centimeters";
            ......
            ......
                }
                ViewBag.message = message;
            }

```
I have to say the ViewBag is convenient, it can pass many kinds of data from Controller to the View.
And in the cshtml file, I can display the result by the use of Razor Code:
```
 @if (ViewBag.message != null)
   {
      <p>@ViewBag.message</p>
   }
```
For the ColorMixer, it's a little bit harder bacause I am not very clear for how to POST data in webpage. I falied when I have a webpage and try to produce some data by click button, then the professor told me I need two methods for this page. One is HttpGte, to get the webpage format befor I submmit any value. Another is HttpPost, to show the result in the page.

The structure of ColorMixer look like:

```
[HttpGet]
public ActionResult ColorMixer(string firstColor, string secondColor)
 {
     return View();
 }
 
  [HttpPost]
 public ActionResult ColorMixerPost(string firstColor, string secondColor)
 {
    Color _firstColor = ColorTranslator.FromHtml(firstColor);
    ......
    ViewBag.thirdItem = thirdColor;

    return View("ColorMixer");
 }
```

The most difficult part for me is how to display the color as block, I add some html code <div> in the ViewPage and set the background use the ViewBage data.

```
 @Html.Label("Second Color")
    <br>
 @Html.TextBox("secondColor", null, htmlAttributes: new { placeholder = "#12HHFF", pattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$" })
    <div style="padding-top:2em">
        @Html.TextBox("mixcolor", "Mix Color", htmlAttributes: new { type = "Submit" })
    </div>
    <div style="padding-top:2em">
        <div class="colorFItem" , style="background-color: @ViewBag.firstItem"></div>
        <div class="colorPItem">@ViewBag.plusItem</div>
        <div class="colorSItem" , style="background-color: @ViewBag.secondItem"></div>
        <div class="colorPItem">@ViewBag.equalItem</div>
        <div class="colorTItem" , style="background-color: @ViewBag.thirdItem"></div>
    </div>
```

#### Merge Branch

As the requirement, I complete the code of MileConvert in hw4-convert branch and complete the ColoeMixer code in another branch, then merge them to master.


After debugging several times, the code works:

When run without input numbers:

![RunWithoutNumber](Code/RunWithoutNumber.jpg)

When run with input some characters:

![RunWithCharacter](Code/RunWithCharacter.jpg)

When run with correct number:

![RunSuccess](Code/RunSuccess.jpg)






