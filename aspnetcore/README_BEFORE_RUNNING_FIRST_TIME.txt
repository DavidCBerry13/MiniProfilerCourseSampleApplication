To run the examples, you need to tell Visual Studio to start two projects when you run the solution.  You will need to do this before you run each project the first time.  

To do this, open the solution in Visual Studio.  Then:
-- Right click on the solution in Solution Explorer
-- Click on the item "Set Startup Projects..."
-- For the projects "InvestmentManager" and "StockIndexWebService" set the action to "Start"

This will start both projects when you run the solution such that the app (InvestmentManager) can make a call out to the API (StockIndexWebService) during a page request

You will need to do this for each version of the app you run.  You only need to do it once per version though, as Visual Studio will save your choice in an .sup file after you make your choice.  

If you forget, you the API call will time out and you will get an error page reminding you of the need to do this.