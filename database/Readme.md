# Database Files
This directory contains the database files needed to make the sample application run.  The database files are distributed as BACPAC files which provide a relatively seamless way to get the databases onto your local system.

You must have SQL Server 2016 or later as your database.  The most common approach will be to import these files to an instance of SQL Server running on you local development workstation.  If you do not have a local install of SQL Server, you can download SQL Server Express from this link: [https://www.microsoft.com/en-us/sql-server/sql-server-editions-express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express)

# Importing the BACPAC Files
There are three databases that you will be importing/creating.  They are:

 - **Stock Data** - This is the main database used by the MVC app
 - **StockExchangeData** - This is the database used by the API project in the solution.
 - **Miniprofiler** - A database where Miniprofiler can write out its data to if you wish to persist the data

To import each database, follow these steps:
1) Connect to the database you want to import the data to using SQL Server Management Studio (SSMS)

2) In SSMS, Right Click on the "Databases" folder and then select "Import Data-tier Application"

![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-1.png)

3) Click the "Browse" button to locate the BACPAC file you wish to import

![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-2.png)

4) Find the BACPAC file on your local disk and select it
![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-3.png)

5) Select the "Next" button to go to the next step
![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-4.png)

6) The defaults you see on this page should be fine, so you can just select "Next".  If you did want to give the database a new name when it was imported, this is where you would do it.
![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-5.png)

7) On the Summary Page select "Finish"
![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-6.png)

8) The database will not be imported.  This may take anywhere from a few seconds to a couple of minutes but is generally pretty fast.  When finished, you will see this screen.
![enter image description here](https://raw.githubusercontent.com/DavidCBerry13/MiniProfilerCourseSampleApplication/master/database/import-images/import-bacpac-7.png)

Repeat these steps for each database


