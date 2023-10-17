using Gotchi.Core.Mangers;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Mangers;

public class CannotAffordPurchaseOfAssetException : CoreMangerException
{
    public CannotAffordPurchaseOfAssetException(Portfolio portfolio, CryptoCoin coin, float amountInValue) 
        : base(
            $"Not enough in balance, PortfolioId : {portfolio.Id}, Coin {coin.Name}, purchase amount ${amountInValue}"
            ){}
}

public class AssetNotFoundException : CoreMangerException
{
    public AssetNotFoundException(Portfolio portfolio, CryptoCoin coin)
        : base(
            $"Asset with CoinMarketId : {coin.Id} not found in portfolio with id : {coin.Id}"
            )
    { }
}
