
using AutoMapper;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;

namespace Gotchi.CryptoCoins.DataAccess;

public class CryptoCoinsDataAccess : ICryptoCoinsDataAccess
{
    private readonly ICryptoCoinManger _cryptoCoinManger;
    private readonly IMapper _mapper;

    public CryptoCoinsDataAccess(ICryptoCoinManger cryptoCoinManger, IMapper mapper)
    {
        _cryptoCoinManger = cryptoCoinManger;
        _mapper = mapper;
    }

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId)
    {
        var cryptoCoinModel = _cryptoCoinManger.CryptoCoinByCoinMarketId(coinMarketId);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public CryptoCoinDTO CryptoCoinByName(string name)
    {
        var cryptoCoinModel = _cryptoCoinManger.CryptoCoinByName(name);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug)
    {
        var cryptoCoinModel = _cryptoCoinManger.CryptoCoinBySlug(slug);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol)
    {
        var cryptoCoinModel = _cryptoCoinManger.CryptoCoinBySymbol(symbol);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }
}
