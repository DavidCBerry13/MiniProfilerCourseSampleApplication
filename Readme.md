# Using MiniProfiler in ASP.NET Core Sample Application
This repository contains the sample application for the Pluralsight course "Using MiniProfiler in ASP.NET Core".  There are snapshots of the code for the end of each clip.

## Requirements

 - The code is written in [ASP.NET Core 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1).  There is no reason it should not work with .NET Core 2.0 or 2.2.
 - For the course, I used Visual Studio 2017 (15.9.4 to be specific).  Anything higher than Visual Studio 2017 v15.7 should be fine.
 - SQL Server 2016 or higher is required for the app databases.

## Running The Sample Application

 - Make sure you have the correct software installed from above
 - Install the databases.  In the databases folder, there are BACPAC files of the databases you need and instructions how to load a BACPAC file into your local SQL Server
 - Update your connection strings.  You need to make sure the connection strings in the InvestmentManager and StockIndexWebService are pointing at where you installed your databases
 - Make sure you have the correct startup projects.  Both InvestmentManager and StockIndexWebService need to be running for the application to work.  Right click on the solution and go to "Set Startup Projects" and make sure both of these are selected

MiniProfiler should show up in the bottom left hand corner of the app.

Note: Sometimes when I have run the solution, I will get a situation where the apps exit immediately after they start running.  This is apparently due to a race condition in IIS Express that sometimes happens, so if you experience this, just run the apps again.