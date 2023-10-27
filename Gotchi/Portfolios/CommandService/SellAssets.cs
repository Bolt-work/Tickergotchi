using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Portfolios.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.CommandService;

public class SellAssetsCommand : ICoreCommand
{
    public readonly string PortfolioId;
    public readonly string CoinMarketId;
    public readonly int Units;

    public SellAssetsCommand(string portfolioId, string coinMarketId, int units)
    {
        PortfolioId = portfolioId;
        CoinMarketId = coinMarketId;
        Units = units;
    }
}

public class SellAssetsCommandHandler : CoreCommandHandlerBase<SellAssetsCommand> 
{
    private IPortfolioManager _portfolioManager;
    private ICryptoCoinManager _cryptoCoinManager;

    public SellAssetsCommandHandler(IPortfolioManager portfolioManager, ICryptoCoinManager cryptoCoinManager, ILogger<SellAssetsCommandHandler> logger) 
        : base(logger)
    {
        _portfolioManager = portfolioManager;
        _cryptoCoinManager = cryptoCoinManager;  
    }

    public override void Handle(SellAssetsCommand command) 
    {
        var portfolio = _portfolioManager.GetByPortfolioId(command.PortfolioId);
        var coin = _cryptoCoinManager.CryptoCoinByCoinMarketId(command.CoinMarketId);
        _portfolioManager.SellAsset(portfolio, coin, command.Units);
        _portfolioManager.Store(portfolio);
    }
}
