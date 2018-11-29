## Homework 9

For this task, we're going to deploy the MVC web application built in HW8 to the cloud and use Azure for both the web application and the database.

## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW9_1819.html)
* [Code Repository](https://github.com/KexinPan/CS460/tree/master/HW9)
* [Azure App Link](https://cs460hw9.azurewebsites.net)
* https://github.com/KexinPan/CS460.git

## Steps

### set up

Create resource group on Azure:
![Create resource group](img/Create resource group.jpg)

Create Database and Database server:
![Create DB and DB server](img/Create DB and DB server.jpg)

Add firewall rules:

![create firewall rule1](img/create firewall rule1.jpg)
![create firewall rule2](img/create firewall rule2.jpg)

### connect to SSMS:

Get the server name:
![connect to SSMS1(get the server name)](img/connect to SSMS1(get the server name).jpg)

Log in SSMS:
![connect to SSMS1(log in SSMS)](img/connect to SSMS1(log in SSMS).jpg)

After connection:
![connect to SSMS1(log in SSMS)](img/connect to SSMS1(after log in).jpg)

Connect the databse to Azure:
![connect to SSMS1(add tables to azure database)](img/connect to SSMS1(add tables to azure database).jpg)

After add tables:
![connect to SSMS1(after add tables)](img/connect to SSMS1(after add tables).jpg)

### connect to Azure database

Add connetion string:
![connect local DB to azure(add connection string)](img/connect local DB to azure(add connection string).jpg)

### deploy app to azure

Create an app on Azure:
![Deploy(create a web app first)](img/Deploy(create a web app first).jpg)

Add an app connetion string:
![Deploy(add an app connection string)](img/Deploy(add an app connection string).jpg)

Deploy the application:
![Deploy(deploy the web app)](img/Deploy(deploy the web app).jpg)

Deploy successfuly:
![success](img/Deploy(success).jpg)


