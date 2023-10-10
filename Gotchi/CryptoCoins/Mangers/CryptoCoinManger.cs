using CoinMarketCap;
using CoinMarketCap.DataModels;
using Gotchi.Core.Helpers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.CryptoCoins.Mangers;

public class CryptoCoinManger : ICryptoCoinManger
{
    private ICryptoCoinRepository _cryptoCoinRepository;
    private ICoinMarketApi _coinMarketApi;
    public CryptoCoinManger(ICryptoCoinRepository cryptoCoinRepository, ICoinMarketApi coinMarketApi)
    {
        _cryptoCoinRepository = cryptoCoinRepository;
        _coinMarketApi = coinMarketApi;
    }

    //public ICollection<CryptoCoin> GetByName(string name) 
    //{
        
    //}

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

}
