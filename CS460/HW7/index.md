## Homework 7

For this task, we're going to write a MVC web application that employs AJAX to build responsive views and use api to build the application.

## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW7_1819.html)
* [Code Repository](https://github.com/KexinPan/CS460/tree/master/HW7)
* https://github.com/KexinPan/CS460.git

### Coding (Get Picture From Object)

The api_key from giphy only works for a short time, after that, I received a error message called "api rate limit exceed", so I have to get a new one and replace the old one. I found a tutorial online, it explains how to get the url of the picture by giphy api, it helps a lot.

Here is the code for this part:

In the action:
```
string api = "http://api.giphy.com/v1/stickers/translate?";
            string apikey = "&api_key="+System.Web.Configuration.WebConfigurationManager.AppSettings["CS460ApiKey"];
            string query = "&s=" + id;
            string url = api + apikey + query;

            //var giphyData = new WebClient().DownloadString(url);

            // same result but transfer to json format
            Stream giphyStream = WebRequest.Create(url).GetResponse().GetResponseStream();

            
            var giphyData = new System.Web.Script.Serialization.JavaScriptSerializer()
                                  .Deserialize<Object>(new StreamReader(giphyStream)
                                  .ReadToEnd());
           return Json(giphyData, JsonRequestBehavior.AllowGet);
```

In the javascript:

```
function displayData(giphyData) {

    var imageUrl = giphyData.data.images.original_still.url;

    $("#Image").append("<img src='" + imageUrl + "' style='width:100px;height:100px; margin:15px;'/>");
}

```

### Coding (Add Boring Word Definition)

I spent a long time on this part, because I changed a lot after this part, I add a for loop to traverse all characters in input box, and I also have troubled in how to split the word by "space". I serached online and found a function in javascript named ".split(string)", I'm still a little bit confused what the function is but it did what I want.

Here is the code for this part:

```
var boringWords = ["I", "I'm", "my", "go", "going", "to", "and", "it", "where", "me", "we", "us", "those","you", "she", "he", "that", "which", "who","whom", "whose", "is", "neither", "but"];

$("#InputBox").bind("keypress", function (e) {

    if (e.key === ' ') {
        var value = $("#InputBox").val();
        var valueSpace = value.split(" ");
        var data = valueSpace[valueSpace.length - 1];
        var flag = false;
        for (var i = 0; i < boringWords.length; i++) {

            if (boringWords[i].toUpperCase() === data.toUpperCase()) {
               flag = true;
            }
        }
        if (flag) {
            $("#Image").append("<label>" + data + "&nbsp;</label></div>");
        }
        else {
            ajaxFuc(data);
        }
    }

});
```
### Coding (Add Database)

It's not hard to build a database in project, everything was similar to the HW5, create a model, create a database, create a database context in DAL and add a connect string in Web.config.

Here is the code of the Model:
```
        public class Request
    {
        [Key]
        public int ID { get; set; }

        public DateTime DateValue { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public string IPAddress { get; set; }

        public string Type { get; set; }

    }
```
Here is the code of the context:

```
public class RequestContext:DbContext
    {
        public RequestContext() : base("name=Requests") { }

        public virtual DbSet<Request> Requests { get; set; }
    }
```
The hard part is how to get ip address and clent type of the user, I asked our classmates and they told me using "Request". I have to say it's smart.

Here is the code of this part:

```
            Request item = new Request();
            item.IPAddress = Request.UserHostAddress;
            item.Description = id;
            item.Type = Request.UserAgent;
            database.Requests.Add(item);
            database.SaveChanges();
```

### Test

Here is the video to show thw work:

* [Video](https://youtu.be/FxQ_sywFzzU)

