using Gotchi.CryptoCoins.DTOs;

namespace Gotchi.CryptoCoins.DataAccess
{
    public interface ICryptoCoinsDataAccess
    {
        CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId);
        CryptoCoinDTO CryptoCoinByName(string name);
        ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug);
        ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol);
    }
}