using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Repository
{
    public interface ICryptoCoinRepository
    {
        bool DeleteAll();
        bool Exists(string id);
        ICollection<CryptoCoin> GetAll();
        CryptoCoin GetByCoinMarketId(string coinMarketId);
        CryptoCoin GetById(string id);
        ICollection<CryptoCoin> GetByName(string name);
        ICollection<CryptoCoin> GetBySlug(string slug);
        ICollection<CryptoCoin> GetBySymbol(string symbol);
        void Insert(IList<CryptoCoin> models);
    }
}