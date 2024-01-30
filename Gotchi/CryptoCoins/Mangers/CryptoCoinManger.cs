using CoinMarketCap;
using CoinMarketCap.DataModels;
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Managers;

public class CryptoCoinManager : CoreManagerBase, ICryptoCoinManager, IDACryptoCoinManager
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
        if (_updating)
            return false;

        _updating = true;
        var jsonModel = _coinMarketApi.CallApi();
        DateTime now = DateTime.UtcNow;
        DateTime nextUpdated = now.AddMinutes(GameSettings.Values().UpdateCoinValuesDbInMinutes);

        var coinList = ParseCoinMarketDataLazyLoad(jsonModel, now, nextUpdated);

        _cryptoCoinRepository.DeleteAll();
        _cryptoCoinRepository.Insert(coinList);

        _updating = false;
        return true;
    }

    private IEnumerable<CryptoCoin> ParseCoinMarketData(RootModel jsonModel, DateTime now, DateTime nextUpdated)
    {
        var data = jsonModel.data ?? throw new CoinMarketApiModelPropertyIsNullExceptions(jsonModel, "data");

        var coinList = new List<CryptoCoin>();
        foreach (var datum in data)
        {
            var coin = ParseDatum(datum, now, nextUpdated);
            coinList.Add(coin);
        }

        return coinList;
    }

    private IEnumerable<CryptoCoin> ParseCoinMarketDataLazyLoad(RootModel jsonModel, DateTime now, DateTime nextUpdated)
    {
        var data = jsonModel.data ?? throw new CoinMarketApiModelPropertyIsNullExceptions(jsonModel, "data");

        foreach (var datum in data)
        {
            yield return ParseDatum(datum, now, nextUpdated);
        }
    }

    private CryptoCoin ParseDatum(Datum datum, DateTime now, DateTime nextUpdated) 
    {
        CryptoCoin coin = new();

        coin.Id = datum.id.ToString();
        coin.Name = datum.name;
        coin.Slug = datum.slug;
        coin.Symbol = datum.symbol;
        coin.CoinMarketLastUpdated = datum.last_updated;
        coin.LastUpdated = now;
        coin.NextUpdated = nextUpdated;

        var quote = datum.quote ?? throw new CoinMarketApiModelPropertyIsNullExceptions(datum, "quote");
        var usd = quote.USD ?? throw new CoinMarketApiModelPropertyIsNullExceptions(quote, "USD");
        coin.Price = usd.price;
        
        return coin;
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

    #region Data Access
    public async Task<CryptoCoin?> CryptoCoinByCoinMarketIdAsync(string coinMarketId)
    {
        CheckToUpdateDatabase();
        return await _cryptoCoinRepository.GetByCoinMarketIdAsync(coinMarketId);
    }
    public async Task<CryptoCoin?> CryptoCoinByNameAsync(string name)
    {
        CheckToUpdateDatabase();
        return await _cryptoCoinRepository.GetByNameAsync(name);
    }
    public async Task<IEnumerable<CryptoCoin>> CryptoCoinBySlugAsync(string slug)
    {
        CheckToUpdateDatabase();
        return await _cryptoCoinRepository.GetBySlugAsync(slug);
    }
    public async Task<IEnumerable<CryptoCoin>> CryptoCoinBySymbolAsync(string symbol)
    {
        CheckToUpdateDatabase();
        return await _cryptoCoinRepository.GetBySymbolAsync(symbol);
    }

    public async Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20)
    {
        CheckToPopulateDatabase();
        return await _cryptoCoinRepository.GetNameSuggestionsAsync(prefix, limit);
    }

    public async Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20)
    {
        CheckToPopulateDatabase();
        return await _cryptoCoinRepository.GetSlugSuggestionsAsync(prefix, limit);
    }

    public async Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20)
    {
        CheckToPopulateDatabase();
        return await _cryptoCoinRepository.GetSymbolSuggestionsAsync(prefix, limit);
    }

    #endregion

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

    private void CheckToPopulateDatabase()
    {
        if (!_cryptoCoinRepository.HasAnyEntries())
        {
            UpdateCoinValues();
        }
    }
}
