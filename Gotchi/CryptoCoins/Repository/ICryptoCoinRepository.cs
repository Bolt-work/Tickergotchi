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
        Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20);
        bool HasAnyEntries();
        void Insert(IEnumerable<CryptoCoin> models);
    }
}