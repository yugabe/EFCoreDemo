DogFarm
-------

These are very simple demo applications to demonstrate the basic functionality of [EF Core](https://docs.microsoft.com/en-us/ef/core/) in simple console applications.

Requirements:
- .NET Core SDK version 6.0.300 or later, available [here](https://dotnet.microsoft.com/download).
- LocalDB DB instance available at the host machine at `(localDB)\MSSQLLocalDB`. LocalDB is installed by default for [Visual Studio](https://visualstudio.microsoft.com/downloads/). If you want to use another database, you need to find and change the appropriate provider from the one used here, which is available from the [Microsoft.EntityFrameWorkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) NuGet package and change the corresponding configurations and connection string.

Running the projects:
- Fork, download or clone the repo to your computer.
- Open the solution in [Visual Studio](https://visualstudio.microsoft.com/downloads/) or the repo's root folder in [VS Code](https://code.visualstudio.com/).
- Run the following command in the selected project's folder to restore the local dotnet tools*: `dotnet tool restore`.
	- (*): This command installs the [dotnet-ef](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) [.NET tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) (not globally). The version is specified in each folder's `.config` subfolder, which contains the dotnet-tools.json file that specifies which tools to restore when ran from that root folder.
- Run the following command in the selected project's folder to create the initial database: `dotnet ef database update`.
- Start the project in Visual Studio with Debugging with `F5` or by running `dotnet run`.

