using CoinMarketCap;
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Managers;

public class CryptoCoinManager : CoreManagerBase, ICryptoCoinManager
{
    private ICryptoCoinRepository _cryptoCoinRepository;
    private ICoinMarketApi _coinMarketApi;

    private static bool _updating = false;
    public CryptoCoinManager(ICryptoCoinRepository cryptoCoinRepository, ICoinMarketApi coinMarketApi)
    {
        _cryptoCoinRepository = cryptoCoinRepository;
        _coinMarketApi = coinMarketApi;
    }

    public bool UpdateCoinValues()
    {
        if(_updating)
            return false;

        _updating = true;
        var jsonModel = _coinMarketApi.CallApi();

        var coins = new List<CryptoCoin>();
        DateTime now = DateTime.UtcNow;

        var data = jsonModel.data ?? throw new CoinMarketApiModelPropertyIsNullExceptions(jsonModel, "data");

        foreach (var datum in data)
        {
            CryptoCoin coin = new();

            coin.Id = datum.id.ToString();
            coin.Name = datum.name;
            coin.Slug = datum.slug;
            coin.Symbol = datum.symbol;
            coin.CoinMarketLastUpdated = datum.last_updated;
            coin.LastUpdated = now;

            var quote = datum.quote ?? throw new CoinMarketApiModelPropertyIsNullExceptions(datum, "quote");
            var usd = quote.USD ?? throw new CoinMarketApiModelPropertyIsNullExceptions(quote, "USD");
            coin.Price = usd.price;

            coins.Add(coin);
        }

        _cryptoCoinRepository.DeleteAll();
        _cryptoCoinRepository.Insert(coins);

        _updating = false;
        return true;
    }


    public CryptoCoin CryptoCoinByCoinMarketId(string coinMarketId) 
    {
        CheckToUpdateDatabase();
        var cryptoCoin = _cryptoCoinRepository.GetByCoinMarketId(coinMarketId);
        return ThrowIfModelNotFound(cryptoCoin, coinMarketId);
    }
    public CryptoCoin CryptoCoinByName(string name) 
    {
        CheckToUpdateDatabase();
        return _cryptoCoinRepository.GetByName(name);
    }
    public IEnumerable<CryptoCoin> CryptoCoinBySlug(string slug) 
    {
        CheckToUpdateDatabase();
        return _cryptoCoinRepository.GetBySlug(slug);
    }
    public IEnumerable<CryptoCoin> CryptoCoinBySymbol(string symbol) 
    {
        CheckToUpdateDatabase();
        return _cryptoCoinRepository.GetBySymbol(symbol);
    }

    private void CheckToUpdateDatabase() 
    {
        if (_cryptoCoinRepository.HasAnyEntries())
        {
            var firstCoin = _cryptoCoinRepository.GetFirstEntry();
            var minsPassed = CoreHelper.NumberOfMinutesPassed(firstCoin.LastUpdated);
            if (minsPassed >= GameSettings.Values().UpdateCoinValuesDbInMinutes)
            {
                UpdateCoinValues();
            }
        }
        else 
        {
            UpdateCoinValues();
        }
    }
}
