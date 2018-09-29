## Homework 1

For the first assignment, we got the task of creating a series of simple web pages and styling them using CSS and Bootstrap. I have studied HTML during summer vacation, but I know nothing about bootstrap and css, so I must start early before class.

I have never used Git before, so the class start, I need to find information from the Internet to preview what git is. On the first day, I was anxious because I was afraid that I couldn’t follow the class, but I feel a little easier when I knew what git was and how to use it.

## Links

*.[Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW1.html)
*.[Code Repository](https://github.com/KexinPan/CS460)
*.[Final Page](https://github.com/KexinPan/CS460/blob/master/HW1/index.html)
*.[https://github.com/KexinPan/CS460.git](https://github.com/KexinPan/CS460.git)

### For Git

I download git online and try some simple command first. I create a folder to store all the files when I learn Git. In this folder, I create first file called README.md. I initialize git and set up my username and account, then I try to add and commit this file and push it to the remote repository.

```
mkdir LearnGit
cd LearnGit
git add index.HTML
git status
git commit -m "a commit message here"
git status
git push origin master
```
### For HTML,CSS,BOOTSTRAP

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




