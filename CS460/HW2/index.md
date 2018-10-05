## Homework 2

The second honework is use javascript and jquery in the html so we can write a webpage while with css and bootstrap for the format. The assignment aslo require create a new branch to do it. 


## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW2.html)
* [Code Repository](https://github.com/KexinPan/CS460)
* [Final Page](https://github.com/KexinPan/KexinPan.github.io/tree/master/CS460/HW2/sample)
* https://github.com/KexinPan/CS460.git

### Setup the environment

The homework requires a new branch to do it. I created two branches because the first time when I finished the assignment, I found I push it to a wrong folder, so I create a branch and did it again.

Here are some code when I create the new branch.

```
git branch hw2
git checkout hw2
```
Then I create new files on this branch. When I complete the code, I switch to the master branch, then merge the hw2 branch.

```
touch index.html
touch css.html
git add .
git commit -m "completed code"
git push origin hw2
git checkout master
git merge hw2
```

### Think of a project and design it

I'm not familiar with HTML, so I moved slow and I searched online to get help. I set up a simple website to finish the requirement of assignment. I have three pages on my website. The basic structure looks like:

```
<html>
    <head></head>
    <body>
    <ul>
        <li>...</li>
        ...
    </ul>
    <div class="container-fluid">
        <div id="left" class="left">
        ...
        </div>
        <div id="left" class="right">
        ...
        </div>
    </div>
    </body>
```


I read the documentation and add the bootstrap link on the head of my html file to use it.
```
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
```
I have my own css file to style my webpage, most times I picked some soft color to make it seems bright and maybe more comfortable.





