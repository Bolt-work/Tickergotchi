using FluentAssertions;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.Mangers;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Portfolios.CommandServices;

public class BuyAssetsCommandHandlerTests
{
    private MockPortfolioRepository _portfolioRepo;
    private PortfolioManger _portfolioManger;
    private CryptoCoinManger _coinManger;

    private ILogger<BuyAssetsCommandHandler> _logger;
    private BuyAssetsCommandHandler _commandHandler;
    public BuyAssetsCommandHandlerTests()
    {
        //Dependencies
        _logger = CommandHandlerHelper.Logger<BuyAssetsCommandHandler>();
        _coinManger = CommandHandlerHelper.CryptoCoinMangerWithData();
        _portfolioRepo = CommandHandlerHelper.MockPortfolioRepository();
        _portfolioManger = CommandHandlerHelper.PortfolioManger(_portfolioRepo);

        _commandHandler = new BuyAssetsCommandHandler(_portfolioManger, _coinManger, _logger);
    }

    [Fact]
    public void Handle_ValidBuyAssetsCommand_CreatesAsset()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new BuyAssetsCommand(portfolioId!, coinId, 100F);

        //Act
        _commandHandler.Handle(command);

        // Assert
        _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Should().Contain(x => x.CoinMarketId == coinId);

        var coin = _coinManger.CryptoCoinByCoinMarketId(coinId!);
        var asset = _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Single(x => x.CoinMarketId == coinId);
       
        asset.Name.Should().Be(coin.Name);
        asset.Slug.Should().Be(coin.Slug);
        asset.PriceWhenLastBought.Should().Be(coin.Price);
        asset.Symbol.Should().Be(coin.Symbol);

        asset.Units.Should().Be(100F);
        asset.Profit.Should().Be(0 - 100F);
    }

    [Fact]
    public void Handle_ValidBuyAssetsCommand_AddsToAsset()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new BuyAssetsCommand(portfolioId!, coinId, 100F);

        //Act
        _commandHandler.Handle(command);
        _commandHandler.Handle(command);

        // Assert
        _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Should().Contain(x => x.CoinMarketId == coinId);

        var coin = _coinManger.CryptoCoinByCoinMarketId(coinId!);
        var asset = _portfolioRepo.GetByPortfolioId(portfolioId!).Assets.Single(x => x.CoinMarketId == coinId);
        asset.Units.Should().Be(200F);
        asset.Profit.Should().Be(0 - 200F);
    }


    [Fact]
    public void Handle_BuyAssetsCommandCommandBalanceToLow_CannotAffordError()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new BuyAssetsCommand(portfolioId!, coinId, float.MaxValue);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<CannotAffordPurchaseOfAssetException>();
    }

    [Fact]
    public void Handle_BuyAssetsCommandCommandBalanceToLow_AmountIsNegative()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new BuyAssetsCommand(portfolioId!, coinId, -1);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Handle_BuyAssetsCommandCommandBalanceToLow_AmountIsZero()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var coinId = "1";
        var command = new BuyAssetsCommand(portfolioId!, coinId, 0);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
