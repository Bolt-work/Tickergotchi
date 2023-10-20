using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Portfolios.Mangers;
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

public class SellAssetsCommandHandler : CoreCommandHandlerBase 
{
    private IPortfolioManger _portfolioManger;
    private ICryptoCoinManger _cryptoCoinManger;

    public SellAssetsCommandHandler(IPortfolioManger portfolioManger, ICryptoCoinManger cryptoCoinManger, ILogger<SellAssetsCommandHandler> logger) 
        : base(logger)
    {
        _portfolioManger = portfolioManger;
        _cryptoCoinManger = cryptoCoinManger;  
    }

    public void Handle(SellAssetsCommand command) 
    {
        base.Handle(command);
        var portfolio = _portfolioManger.GetByPortfolioId(command.PortfolioId);
        var coin = _cryptoCoinManger.CryptoCoinByCoinMarketId(command.CoinMarketId);
        _portfolioManger.SellAsset(portfolio, coin, command.Units);
        _portfolioManger.Store(portfolio);
    }
}
