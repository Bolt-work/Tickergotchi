using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Repository
{
    public interface ICryptoCoinRepository
    {
        bool DeleteAll();
        bool Exists(string coinMarketId);
        IEnumerable<CryptoCoin> GetAll();
        CryptoCoin GetByCoinMarketId(string coinMarketId);
        CryptoCoin GetByName(string name);
        IEnumerable<CryptoCoin> GetBySlug(string slug);
        IEnumerable<CryptoCoin> GetBySymbol(string symbol);
        void Insert(IList<CryptoCoin> models);
    }
}