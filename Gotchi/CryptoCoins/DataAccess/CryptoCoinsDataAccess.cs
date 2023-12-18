
using AutoMapper;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.CryptoCoins.Managers;
using Gotchi.CryptoCoins.Repository;

namespace Gotchi.CryptoCoins.DataAccess;

public class CryptoCoinsDataAccess : ICryptoCoinsDataAccess
{
    private readonly ICryptoCoinManager _cryptoCoinManager;
    private readonly IMapper _mapper;

    public CryptoCoinsDataAccess(ICryptoCoinManager cryptoCoinManager, IMapper mapper)
    {
        _cryptoCoinManager = cryptoCoinManager;
        _mapper = mapper;
    }

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId)
    {
        var cryptoCoinModel = _cryptoCoinManager.CryptoCoinByCoinMarketId(coinMarketId);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public CryptoCoinDTO CryptoCoinByName(string name)
    {
        var cryptoCoinModel = _cryptoCoinManager.CryptoCoinByName(name);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug)
    {
        var cryptoCoinModel = _cryptoCoinManager.CryptoCoinBySlug(slug);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol)
    {
        var cryptoCoinModel = _cryptoCoinManager.CryptoCoinBySymbol(symbol);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }

    public async Task<CryptoCoinDTO?> CryptoCoinByCoinMarketIdAsync(string coinMarketId)
    {
        if (string.IsNullOrWhiteSpace(coinMarketId))
            return null;

        var cryptoCoinModel = await _cryptoCoinManager.CryptoCoinByCoinMarketIdAsync(coinMarketId);
        if(cryptoCoinModel is null)
            return null;

        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public async Task<CryptoCoinDTO?> CryptoCoinByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var cryptoCoinModel = await _cryptoCoinManager.CryptoCoinByNameAsync(name);
        if (cryptoCoinModel is null)
            return null;

        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySlugAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return new List<CryptoCoinDTO>();

        var cryptoCoinModel = await _cryptoCoinManager.CryptoCoinBySlugAsync(slug);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }

    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySymbolAsync(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return new List<CryptoCoinDTO>();

        var cryptoCoinModel = await _cryptoCoinManager.CryptoCoinBySymbolAsync(symbol);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }
}
