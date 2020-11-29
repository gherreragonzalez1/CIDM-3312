# CIDM 3312 Notes

ASP.NET Core 3.x Stuff

## Using Git

We'll want to create a local repo.

* (do this just once): `git init`
* (do this just once): `git remote add origin https://github.com/gherreragonzalez1/CIDM3312.git`
* (do this just once): `dotnet new gitignore --output <Name>`
* (do this whenever there are new files): `git add -A`
* (do this on every commit - kinda like a save): ` git init -m 'a descriptive message indicating what is new'`
* (do this every time you want to save to the server): `git push -u origin master`

# Command List

## Creating a Repository + New Project Template on ~Documents/Code/{folder}

* Create a **README.MD** file inside ~Documents/Code/{folder}
    1. `dotnet new globaljson --sdk-version 3.1.402 --output {folder}` (just once)
    2. `git init` (do this just once inside ~Documents/Code)
    3. `git add -A` (do this whenever there are new files)
    4. `git commit -m 'added README.MD'` (do this after every git add -A)
    5. `git branch -M master`
    6. `git remote add origin https://github.com/gherreragonzalez1/{repository}.git` (just once)
    7. `git push -u origin master` (do this everytime you want to upload to GitHub)
    8. `dotnet new gitignore --output {folder}` (just once for every project)
    9. `dotnet new --no-https --output {folder} --framework netcoreapp3.1` (just once for every project)
        * This command creates the contents of the web project

## Creating a Repository + Commiting to GitHub

* Create a **README.MD** file inside ~Documents/Code
    1. `git init`
    2. `git add -A`
    3. `git commit -m 'added README.MD'`
    4. `git branch -M master`
    5. `git remote add origin https://github.com/gherreragonzalez1/{repository}.git`
    6. `git push -u origin master`

## Creating a New Project Template (GitHub is already set up at this point)
