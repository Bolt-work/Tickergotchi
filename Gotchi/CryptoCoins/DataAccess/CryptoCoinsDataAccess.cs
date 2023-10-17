
using AutoMapper;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.CryptoCoins.Repository;

namespace Gotchi.CryptoCoins.DataAccess;

public class CryptoCoinsDataAccess : ICryptoCoinsDataAccess
{
    private readonly ICryptoCoinRepository _cryptoCoinRepository;
    private readonly IMapper _mapper;

    public CryptoCoinsDataAccess(ICryptoCoinRepository cryptoCoinRepository, IMapper mapper)
    {
        _cryptoCoinRepository = cryptoCoinRepository;
        _mapper = mapper;
    }

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId)
    {
        var cryptoCoinModel = _cryptoCoinRepository.GetByCoinMarketId(coinMarketId);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public CryptoCoinDTO CryptoCoinByName(string name)
    {
        var cryptoCoinModel = _cryptoCoinRepository.GetByName(name);
        return _mapper.Map<CryptoCoinDTO>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug)
    {
        var cryptoCoinModel = _cryptoCoinRepository.GetBySlug(slug);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }

    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol)
    {
        var cryptoCoinModel = _cryptoCoinRepository.GetBySymbol(symbol);
        return _mapper.Map<ICollection<CryptoCoinDTO>>(cryptoCoinModel);
    }
}
