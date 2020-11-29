# Steps to run this

1. At the top of *VATSIMData* make sure we've created a globaljson file `dotnet new globaljson --sdk-version 3.1.403`
2. Make a new directory called webapp and run this inside of it: `dotnet new web --no-https --framework netcoreapp3.1`
3. Create models (we already have the **data** project, so we're good here)
4. Add references inside webapp directory
    1. `dotnet add reference ../library/library.csproj`
5. Add the following packages to the webapp folder
    1. `dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.10`
    2. `dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.10`
    3. `dotnet tool uninstall --global dotnet-ef` <-- gets rid of existing version
    4. `dotnet tool install --global dotnet-ef --version 3.1.10` <-- current version (NOT .NET 5)
6. Freeman uses **LibMab** to install front-end dependencies
    1. `dotnet tool uninstall --global Microsoft.Web.LibraryManager.Cli` <-- gets rid of the old version
    2. `dotnet tool install --global Microsoft.Web.LibraryManager.Cli --version 2.1.113`
    3. Install Twitter bootstrap:
        1. `libman init -p cdnjs`
        2. `libman install twitter-bootstrap@4.5.3 -d wwwroot/lib/twitter-bootstrap`
