SELECT max(tradeDate) from TradeDates;

SELECT * FROM AccountPositions
    WHERE TradeDate = '2018-10-17';



SELECT * FROM AccountPositions
    WHERE Ticker = '$$$$'
	ORDER BY TradeDate;


UPDATE AccountPositions
    SET Shares = MarketValue,
	    Price = 1.0
	WHERE Ticker = '$CASH'

UPDATE AccountPositions
    SET Ticker = '$$$$'
	WHERE Ticker = '$CASH'

DELETE FROM Securities WHERE Ticker = '$CASH'
SELECT *
    FROM Securities
	WHERE Ticker = '$CASH'

INSERT INTO Securities (Ticker, SecurityTypeCode, SecurityName, Description, Sector, Industry)
    VALUES ('$$$$', 'CASH', 'Cash', 'Cash in account', 'Cash', 'Cash')




CREATE TABLE AccountMarketValues
(
    TradeDate      DATE           NOT NULL,
	AccountNumber  VARCHAR(10)    NOT NULL,
	MarketValue    DECIMAL(12,4)  NOT NULL,
	CONSTRAINT PK_AccountMarketValues
	    PRIMARY KEY (TradeDate, AccountNumber),
	CONSTRAINT FK_AccountMarketValues_TradeDate
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate),
	CONSTRAINT FK_AccountMarketValues_AccountNumber
	    FOREIGN KEY (AccountNumber) REFERENCES Accounts (AccountNumber)
)

SELECT * FROM AccountMarketValues;

INSERT INTO AccountMarketValues (TradeDate, AccountNumber, MarketValue)
SELECT DISTINCT
    TradeDate,
	AccountNumber,
	SUM(MarketValue) OVER (PARTITION BY TradeDate, AccountNumber)
FROM AccountPositions;

