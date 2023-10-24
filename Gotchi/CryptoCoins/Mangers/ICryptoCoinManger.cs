using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Managers
{
    public interface ICryptoCoinManager
    {
        bool UpdateCoinValues();

        CryptoCoin CryptoCoinByCoinMarketId(string coinMarketId);
        CryptoCoin CryptoCoinByName(string name);
        IEnumerable<CryptoCoin> CryptoCoinBySlug(string slug);
        IEnumerable<CryptoCoin> CryptoCoinBySymbol(string symbol);
    }
}