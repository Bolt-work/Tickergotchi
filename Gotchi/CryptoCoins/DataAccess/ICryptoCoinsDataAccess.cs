using Gotchi.CryptoCoins.DTOs;

namespace Gotchi.CryptoCoins.DataAccess
{
    public interface ICryptoCoinsDataAccess
    {
        Task<CryptoCoinDTO?> CryptoCoinByCoinMarketIdAsync(string coinMarketId);
        Task<CryptoCoinDTO?> CryptoCoinByNameAsync(string name);
        Task<ICollection<CryptoCoinDTO>> CryptoCoinBySlugAsync(string slug);
        Task<ICollection<CryptoCoinDTO>> CryptoCoinBySymbolAsync(string symbol);
        Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20);
    }
}