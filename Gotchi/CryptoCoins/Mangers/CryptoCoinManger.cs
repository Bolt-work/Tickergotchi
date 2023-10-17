using CoinMarketCap;
using Gotchi.Core.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;
namespace Gotchi.CryptoCoins.Mangers;

public class CryptoCoinManger : CoreMangerBase, ICryptoCoinManger
{
    private ICryptoCoinRepository _cryptoCoinRepository;
    private ICoinMarketApi _coinMarketApi;
    public CryptoCoinManger(ICryptoCoinRepository cryptoCoinRepository, ICoinMarketApi coinMarketApi)
    {
        _cryptoCoinRepository = cryptoCoinRepository;
        _coinMarketApi = coinMarketApi;
    }

    public bool UpdateCoinValues()
    {
        var jsonModel = _coinMarketApi.CallApi();

        var coins = new List<CryptoCoin>();
        DateTime now = DateTime.Now;

        foreach (var datum in jsonModel.data)
        {
            CryptoCoin coin = new()
            {
                Id = datum.id.ToString(),
                Name = datum.name,
                Slug = datum.slug,
                Symbol = datum.symbol,
                CoinMarketLastUpdated = datum.last_updated,
                LastUpdated = now,
                Price = datum.quote.USD.price
            };

            coins.Add(coin);
        }

        _cryptoCoinRepository.DeleteAll();
        _cryptoCoinRepository.Insert(coins);

        return true;
    }

    public CryptoCoin CryptoCoinByCoinMarketId(string coinMarketId) 
    {
        var cryptoCoin = _cryptoCoinRepository.GetByCoinMarketId(coinMarketId);
        return ThrowIfModelNull(cryptoCoin, coinMarketId);
    } 
    public CryptoCoin CryptoCoinByName(string name) => _cryptoCoinRepository.GetByName(name);
    public ICollection<CryptoCoin> CryptoCoinBySlug(string slug) => _cryptoCoinRepository.GetBySlug(slug);
    public ICollection<CryptoCoin> CryptoCoinBySymbol(string symbol) => _cryptoCoinRepository.GetBySymbol(symbol);

}
