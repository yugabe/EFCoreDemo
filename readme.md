DogFarm
-------

These are very simple demo applications to demonstrate the basic functionality of [EF Core](https://docs.microsoft.com/en-us/ef/core/) in simple console applications.

Requirements:
- .NET Core SDK version 3.1.102 available at the time of writing [here](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x64-installer).
- LocalDB DB instance available at the host machine at `(localDB)\MSSQLLocalDB`. LocalDB is installed by default for [Visual Studio](https://visualstudio.microsoft.com/downloads/). If you want to use another database, you need to find and change the appropriate provider from the one used here, which is available from the [Microsoft.EntityFrameWorkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) NuGet package.

Running the projects:
- Fork, download or clone the repo to your computer.
- Open the solution in [Visual Studio](https://visualstudio.microsoft.com/downloads/) or the repo's root folder in [VS Code](https://code.visualstudio.com/).
- Run the following command in the selected project's folder to restore the local dotnet tools: `dotnet tool restore`.
- Run the following command in the selected project's folder to create the initial database: `dotnet ef database update`.
- Start the project in Visual Studio with Debugging with `F5` or by running `dotnet run`.