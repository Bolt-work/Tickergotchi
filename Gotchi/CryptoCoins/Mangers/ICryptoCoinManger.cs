using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Mangers
{
    public interface ICryptoCoinManger
    {
        bool UpdateCoinValues();

        CryptoCoin CryptoCoinByCoinMarketId(string coinMarketId);
        CryptoCoin CryptoCoinByName(string name);
        ICollection<CryptoCoin> CryptoCoinBySlug(string slug);
        ICollection<CryptoCoin> CryptoCoinBySymbol(string symbol);
    }
}