## Homework 2

The second honework is use javascript and jquery in the html so we can write a webpage while with css and bootstrap for the format. The assignment aslo require create a new branch to do it. 


## Links

* [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW2.html)
* [Code Repository](https://github.com/KexinPan/CS460)
* [Final Page](https://kexinpan.github.io/CS460/HW2/sample)
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

The first part of my application design is to create a wireframe as the basic layout of my application and will guide my development. I decide to design a BMI calculator, the layout will not be complex because I think it's useful.

I used the [Pencil Project](http://pencil.evolus.vn/) application for my design. This tool is easy to use and it's free.

Here are my wireframes:

#### Basic layout

![wireframe-normal](img/wireframe-normal.jpg)

#### Input Error

![inputError](img/inputError.jpg)

#### Submmit Error

![alert](img/alert.jpg)

### Step 4: Creating the Working Page









