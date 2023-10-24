
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
}
