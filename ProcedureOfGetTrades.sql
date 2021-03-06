﻿USE [CrossExchangeDb]
GO 

/****** Object:  StoredProcedure [dbo].[GetTrades]    Script Date: 10/09/2018 03:45:34 م ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTrades]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTrades]
GO
/****** Object:  StoredProcedure [dbo].[GetTrades]    Script Date: 10/09/2018 03:45:34 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTrades]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [dbo].[GetTrades]
	@PortfolioId int

As
Begin
SELECT * FROM
((
select top 1 Trades.* from Trades 
inner join Shares on Shares.Symbol=Trades.Symbol
inner join Portfolios on Portfolios.Id=Trades.PortfolioId
where Action=''BUY'' 
)
union 
(
select top 1 Tr.* from Trades Tr
inner join Shares on Shares.Symbol=Tr.Symbol
inner join Portfolios on Portfolios.Id=Tr.PortfolioId
where Action=''SELL''
and Tr.NoOfShares<(select sum(Trades.NoOfShares) from Trades where Action=''BUY'' and Tr.Symbol=Trades.Symbol )
)
) Trades
End' 
END
GO
