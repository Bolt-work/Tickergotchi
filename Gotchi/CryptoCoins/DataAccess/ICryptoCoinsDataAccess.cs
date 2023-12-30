using Gotchi.CryptoCoins.DTOs;

namespace Gotchi.CryptoCoins.DataAccess
{
    public interface ICryptoCoinsDataAccess
    {
        CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId);
        Task<CryptoCoinDTO?> CryptoCoinByCoinMarketIdAsync(string coinMarketId);
        CryptoCoinDTO CryptoCoinByName(string name);
        Task<CryptoCoinDTO?> CryptoCoinByNameAsync(string name);
        ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug);
        Task<ICollection<CryptoCoinDTO>> CryptoCoinBySlugAsync(string slug);
        ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol);
        Task<ICollection<CryptoCoinDTO>> CryptoCoinBySymbolAsync(string symbol);
        Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20);
        Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20);
    }
}