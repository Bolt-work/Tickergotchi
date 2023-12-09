using Gotchi.Core.Managers;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Managers;

public class CannotAffordPurchaseOfAssetException : CoreManagerException
{
    public CannotAffordPurchaseOfAssetException(Portfolio portfolio, CryptoCoin coin, float amountInValue) 
        : base(
            $"Not enough in balance, PortfolioId : {portfolio.Id}, Coin {coin.Name}, purchase amount ${amountInValue}"
            ){}
}

public class NotEnoughFundsForWithdrawException : CoreManagerException
{
    public NotEnoughFundsForWithdrawException(Portfolio portfolio, float amountToWithdraw)
        : base(
            $"Not enough funds for withdrawal, PortfolioId : {portfolio.Id}, Amount -{amountToWithdraw}"
            )
    { }
}

public class AssetNotFoundException : CoreManagerException
{
    public AssetNotFoundException(Portfolio portfolio, string coinMarketId)
        : base(
            $"Asset with CoinMarketId : {coinMarketId} not found in portfolio with id : {coinMarketId}"
            )
    { }
}

public class AssetDoesHaveEnoughUnitsToSell : CoreManagerException
{
    public AssetDoesHaveEnoughUnitsToSell(Asset asset, float units)
        : base(
            $"Asset with CoinMarketId : {asset.CoinMarketId} only has {asset.Units} units. Can not sell {units} units"
            )
    { }
}