using System;
using System.Threading.Tasks;
using CrossExchange.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace CrossExchange.Tests
{
    public class PortfoliosControllerTests
    {
        private IShareRepository _shareRepository { get; set; }
        private ITradeRepository _tradeRepository { get; set; }
        private IPortfolioRepository _portfolioRepository { get; set; }
        private readonly PortfolioController _PortfolioController;

        public PortfoliosControllerTests()
        {
            _PortfolioController = new PortfolioController(_shareRepository, _tradeRepository, _portfolioRepository);
        }
        [Test]
        public async Task Post_ShouldInsertHourlySharePrice()
        {
            var Portfolio = new Portfolio
            {
                Name = "Portfolio Test",
              
            };

            // Arrange

            // Act
            var result = await _PortfolioController.Post(Portfolio);
            // Assert
            Assert.NotNull(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
        
    }
}
