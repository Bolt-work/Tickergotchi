using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Repository
{
    public interface ICryptoCoinRepository
    {
        bool DeleteAll();
        bool Exists(string coinMarketId);
        IEnumerable<CryptoCoin> GetAll();
        CryptoCoin GetByCoinMarketId(string coinMarketId);
        Task<CryptoCoin?> GetByCoinMarketIdAsync(string coinMarketId);
        CryptoCoin GetByName(string name);
        Task<CryptoCoin?> GetByNameAsync(string name);
        IEnumerable<CryptoCoin> GetBySlug(string slug);
        Task<IEnumerable<CryptoCoin>> GetBySlugAsync(string slug);
        IEnumerable<CryptoCoin> GetBySymbol(string symbol);
        Task<IEnumerable<CryptoCoin>> GetBySymbolAsync(string symbol);
        CryptoCoin GetFirstEntry();
        bool HasAnyEntries();
        void Insert(IList<CryptoCoin> models);
    }
}