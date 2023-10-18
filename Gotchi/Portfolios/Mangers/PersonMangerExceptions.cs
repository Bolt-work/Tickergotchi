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
    public AssetNotFoundException(Portfolio portfolio, string coinMarketId)
        : base(
            $"Asset with CoinMarketId : {coinMarketId} not found in portfolio with id : {coinMarketId}"
            )
    { }
}

public class AssetDoesHaveEnoughUnitsToSell : CoreMangerException
{
    public AssetDoesHaveEnoughUnitsToSell(Asset asset, int units)
        : base(
            $"Asset with CoinMarketId : {asset.CoinMarketId} only has {asset.Units} units. Can not sell {units} units"
            )
    { }
}