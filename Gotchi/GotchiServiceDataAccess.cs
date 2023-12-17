using Gotchi.Core.Services;
using Gotchi.CryptoCoins.DataAccess;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Gotchis.DataAccess;
using Gotchi.Gotchis.DTOs;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.DTOs;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.DTOs;
using Microsoft.Extensions.Logging;

namespace Gotchi;

public class GotchiServiceDataAccess: IPersonDataAccess, 
                                      IPortfolioDataAccess,
                                      ICryptoCoinsDataAccess,
                                      IGotchiDataAccess
{
    private IPersonDataAccess _personDataAccess;
    private IPortfolioDataAccess _portfolioDataAccess;
    private ICryptoCoinsDataAccess _cryptoCoinsDataAccess;
    private IGotchiDataAccess _gotchiDataAccess;

    private ILogger<GotchiServiceDataAccess> _logger;

    public GotchiServiceDataAccess(ILogger<GotchiServiceDataAccess> logger, IPersonDataAccess personDataAccess,
        IPortfolioDataAccess portfolioDataAccess,
        ICryptoCoinsDataAccess cryptoCoinsDataAccess,
        IGotchiDataAccess gotchiDataAccess)
    {
        _logger = logger;
        _personDataAccess = personDataAccess;
        _portfolioDataAccess = portfolioDataAccess;
        _cryptoCoinsDataAccess = cryptoCoinsDataAccess;
        _gotchiDataAccess = gotchiDataAccess;
    }

    public PersonDTO PersonById(string? id) => ICL(_personDataAccess.PersonById, id, null!);
    public async Task<PersonDTO?> PersonByIdAsync(string? id) => await _personDataAccess.PersonByIdAsync(id);
    public PersonDTO PersonByUserName(string? userName) => ICL(_personDataAccess.PersonByUserName, userName, null!);
    public async Task<PersonDTO?> PersonByUserNameAsync(string? userName) => await _personDataAccess.PersonByUserNameAsync(userName);
    public ICollection<PersonDTO> PersonsAll() => _personDataAccess.PersonsAll();
    public async Task<bool> CheckPasswordAndUserNameAsync(string? password, string? userName) => await _personDataAccess.CheckPasswordAndUserNameAsync(password, userName);
    public async Task<bool> CheckPasswordAndPersonIdAsync(string? password, string? personId) => await _personDataAccess.CheckPasswordAndPersonIdAsync(password, personId);
    public async Task<bool> DoesUserNameAlreadyExistAsync(string? userName) => await _personDataAccess.DoesUserNameAlreadyExistAsync(userName);

    public PortfolioDTO PortfolioById(string portfolioId) => ICL(_portfolioDataAccess.PortfolioById, portfolioId, null!);
    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId) => _portfolioDataAccess.PortfoliosByPersonId(personId);
    public ICollection<PortfolioDTO> PortfoliosAll() => _portfolioDataAccess.PortfoliosAll();

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId) => ICL(_cryptoCoinsDataAccess.CryptoCoinByCoinMarketId, coinMarketId, null!);
    public CryptoCoinDTO CryptoCoinByName(string name) => ICL(_cryptoCoinsDataAccess.CryptoCoinByName, name, null!);
    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug) => _cryptoCoinsDataAccess.CryptoCoinBySlug(slug);
    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol) => _cryptoCoinsDataAccess.CryptoCoinBySymbol(symbol);

    public GotchiDTO GotchiById(string gotchiId) => ICL(_gotchiDataAccess.GotchiById, gotchiId, null!);
    public ICollection<GotchiDTO> GotchisByOwnerId(string ownerId) => _gotchiDataAccess.GotchisByOwnerId(ownerId);
    public ICollection<GotchiDTO> GotchisAll() => _gotchiDataAccess.GotchisAll();

    //Invoke, Catch, Log - dear god i need to fix this!
    private T ICL<T>(Func<T> method,T error) 
    {
        try 
        {
            return method();
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error");
            return error;
        }
    }

    private T ICL<T>(Func<string, T> method, string arg, T error)
    {
        try
        {
            return method(arg);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            return error;
        }
    }
}
