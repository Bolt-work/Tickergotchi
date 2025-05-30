﻿using Gotchi.Portfolios.Models;

namespace Gotchi.CryptoCoins.Mangers;

public interface IDACryptoCoinManager
{
    Task<CryptoCoin?> CryptoCoinByCoinMarketIdAsync(string coinMarketId);
    Task<CryptoCoin?> CryptoCoinByNameAsync(string name);
    Task<IEnumerable<CryptoCoin>> CryptoCoinBySlugAsync(string slug);
    Task<IEnumerable<CryptoCoin>> CryptoCoinBySymbolAsync(string symbol);
    Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20);
    Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20);
    Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20);
}
