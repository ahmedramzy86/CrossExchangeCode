using System;
using System.Threading.Tasks;
using CrossExchange.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace CrossExchange.Tests
{
    public class TradeControllerTests
    {
        private IShareRepository _shareRepository { get; set; }
        private ITradeRepository _tradeRepository { get; set; }
        private IPortfolioRepository _portfolioRepository { get; set; }
        private readonly TradeController _tradController;

        public TradeControllerTests()
        {
            _tradController = new TradeController(_shareRepository, _tradeRepository, _portfolioRepository);
        }
        [Test]
        public async Task Post_ShouldInsertHourlySharePrice()
        {
            var trade = new TradeModel
            {
                Symbol = "CBI",
                NoOfShares = 80,
                Price = 60,
                PortfolioId = 1,
                Action= "BUY"
            };

            // Arrange

            // Act
            var result = await _tradController.Post(trade);
            // Assert
            Assert.NotNull(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
        
    }
}
