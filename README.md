# cinema-management-system_schoolwork 

## From Final Report

### 4.2	STEP-BY-STEP INSTALLATION GUIDE

To deploy the Cinema Management System, follow these detailed steps:

Environment Setup:
- Visual Studio: Install Visual Studio 2026. During installation, ensure the ".NET Desktop Development" workload is selected. This provides the necessary compilers and libraries for Windows Forms.
- Database Engine: Install SQL Server 2022 Express or Developer Edition, along with SQL Server Management Studio (SSMS) to manage the data layer.


Database Restoration:
- Open SSMS and connect to your local server instance.
- Open the provided SQL scripts in [Database](Database) -> click Execute to generate the database schema and sample data.


NuGet Packages Installation:
- Open the solution file (.sln) in Visual Studio.
- Right-click on the Project, select “Manage NuGet Packages for Solution…”. Search and install “EntityFramework”, make sure the version is the latest.


Data Model Connection:
- Right-click on the Project, add a new item: Data -> ADO.NET Entity Data Model.
- Select “EF Designer from database” and configure the connection string to point to your local SQL Server instance.
- Update the Server parameter (usually . or localhost or your computer name) to match your local SQL Server name.
- Select the required tables to generate the data context classes.


Build and Run:
- Right-click on the Solution, perform a Clean and then Build to ensure all binaries are correctly generated.
- Go to bin -> Debug -> copy and paste the provided [Posters](Posters) folder
- Press F5/Ctrl + F5 or click the Start/Start without debugging button.
- The system will launch the frmMain screen. Use the default credentials provided in the documentation to gain access.
