using FluentAssertions;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.Managers;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Portfolios.CommandServices;

public class SellAssetsCommandHandlerTests
{
    private MockPortfolioRepository _portfolioRepo;
    private PortfolioManager _portfolioManager;
    private CryptoCoinManager _coinManager;

    private ILogger<SellAssetsCommandHandler> _logger;
    private SellAssetsCommandHandler _commandHandler;

    public SellAssetsCommandHandlerTests()
    {
        //Dependencies
        _logger = CommandHandlerHelper.Logger<SellAssetsCommandHandler>();
        _coinManager = CommandHandlerHelper.CryptoCoinManagerWithData();
        _portfolioRepo = CommandHandlerHelper.MockPortfolioRepository();
        _portfolioManager = CommandHandlerHelper.PortfolioManager(_portfolioRepo);

        _commandHandler = new SellAssetsCommandHandler(_portfolioManager, _coinManager, _logger);
    }

    [Fact]
    public void Handle_ValidSellAssetsCommand_CreatesAsset()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolioAndAsset();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new SellAssetsCommand(portfolioId!, coinId, 100);

        //Act
        _commandHandler.Handle(command);

        // Assert
        _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Should().Contain(x => x.CoinMarketId == coinId);

        var coin = _coinManager.CryptoCoinByCoinMarketId(coinId!);
        var asset = _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Single(x => x.CoinMarketId == coinId);

        asset.Name.Should().Be(coin.Name);
        asset.Slug.Should().Be(coin.Slug);
        asset.PriceWhenLastBought.Should().Be(coin.Price);
        asset.Symbol.Should().Be(coin.Symbol);

        asset.Units.Should().Be(1900);
        asset.Profit.Should().Be((100 * coin.Price) - 2000F);
    }

    [Fact]
    public void Handle_InvalidSellAssetsCommand_AssetNotFound()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new SellAssetsCommand(portfolioId!, coinId, 100);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<AssetNotFoundException>();
    }

    [Fact]
    public void Handle_InvalidSellAssetsCommand_NotEnoughUnits()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolioAndAsset();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new SellAssetsCommand(portfolioId!, coinId, int.MaxValue);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<AssetDoesHaveEnoughUnitsToSell>();
    }

    [Fact]
    public void Handle_InvalidSellAssetsCommand_UnitsIsNegative()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolioAndAsset();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new SellAssetsCommand(portfolioId!, coinId, -1);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Handle_InvalidSellAssetsCommand_UnitsIsZero()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolioAndAsset();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new SellAssetsCommand(portfolioId!, coinId, 0);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
