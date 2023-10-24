using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Portfolios.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.CommandService;

public class BuyAssetsCommand : ICoreCommand
{
    public readonly string PortfolioId;
    public readonly string CoinMarketId;
    public readonly float AmountInValue;

    public BuyAssetsCommand(string portfolioId, string coinMarketId, float amount)
    {
        PortfolioId = portfolioId;
        CoinMarketId = coinMarketId;
        AmountInValue = amount;
    }
}

public class BuyAssetsCommandHandler : CoreCommandHandlerBase
{
    private IPortfolioManager _portfolioManager;
    private ICryptoCoinManager _cryptoCoinManager;

    public BuyAssetsCommandHandler(IPortfolioManager portfolioManager, ICryptoCoinManager cryptoCoinManager, ILogger<BuyAssetsCommandHandler> logger)
        :base(logger)
    {
        _portfolioManager = portfolioManager;
        _cryptoCoinManager  = cryptoCoinManager;
    }

    public void Handle(BuyAssetsCommand command) 
    {
        base.Handle(command);
        var cryptoCoin = _cryptoCoinManager.CryptoCoinByCoinMarketId(command.CoinMarketId);
        var portfolio = _portfolioManager.GetByPortfolioId(command.PortfolioId);
        _portfolioManager.BuyAsset(portfolio, cryptoCoin, command.AmountInValue);
        _portfolioManager.Store(portfolio);
    }
}
