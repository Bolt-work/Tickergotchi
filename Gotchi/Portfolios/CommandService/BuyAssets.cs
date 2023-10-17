using Amazon.Runtime.Internal.Util;
using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Portfolios.Mangers;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

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
    private IPortfolioManger _portfolioManger;
    private ICryptoCoinManger _cryptoCoinManger;

    public BuyAssetsCommandHandler(IPortfolioManger portfolioManger, ICryptoCoinManger cryptoCoinManger, ILogger<BuyAssetsCommandHandler> logger)
        :base(logger)
    {
        _portfolioManger = portfolioManger;
        _cryptoCoinManger  = cryptoCoinManger;
    }

    public void Handle(BuyAssetsCommand command) 
    {
        base.Handle(command);
        var cryptoCoin = _cryptoCoinManger.CryptoCoinByCoinMarketId(command.CoinMarketId);
        var portfolio = _portfolioManger.GetByPortfolioId(command.PortfolioId);
        _portfolioManger.BuyAsset(portfolio, cryptoCoin, command.AmountInValue);
        _portfolioManger.Store(portfolio);
    }
}
