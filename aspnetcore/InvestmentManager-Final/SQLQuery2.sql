SELECT *
    FROM AccountCashFlows cf
	INNER JOIN CashFlowTypes ft
	    ON cf.CashFlowTypeCode = ft.CashFlowTypeCode
	WHERE AccountNumber = 'A161370010'
	    AND ft.ExternalFlow = 1;


SELECT *
    FROM TradeDates
	WHERE MonthEndDate = 1;


SELECT * FROM AccountMarketValues 
WHERE AccountNumber = 'A161370010'
ORDER BY TradeDate;