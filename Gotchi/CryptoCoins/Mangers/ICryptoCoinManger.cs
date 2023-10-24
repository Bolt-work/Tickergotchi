using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Mangers
{
    public interface ICryptoCoinManger
    {
        bool UpdateCoinValues();

        CryptoCoin CryptoCoinByCoinMarketId(string coinMarketId);
        CryptoCoin CryptoCoinByName(string name);
        IEnumerable<CryptoCoin> CryptoCoinBySlug(string slug);
        IEnumerable<CryptoCoin> CryptoCoinBySymbol(string symbol);
    }
}