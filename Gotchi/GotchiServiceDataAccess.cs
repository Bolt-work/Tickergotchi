using Gotchi.Core.Services;
using Gotchi.CryptoCoins.DataAccess;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Gotchis.DataAccess;
using Gotchi.Gotchis.DTOs;
using Gotchi.HighScores.DataAccess;
using Gotchi.HighScores.DTOs;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.DTOs;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.DTOs;
using Microsoft.Extensions.Logging;

namespace Gotchi;

public class GotchiServiceDataAccess: IPersonDataAccess, 
                                      IPortfolioDataAccess,
                                      ICryptoCoinsDataAccess,
                                      IGotchiDataAccess,
                                      IHighScoreDataAccess
{
    private IPersonDataAccess _personDataAccess;
    private IPortfolioDataAccess _portfolioDataAccess;
    private ICryptoCoinsDataAccess _cryptoCoinsDataAccess;
    private IGotchiDataAccess _gotchiDataAccess;
    private IHighScoreDataAccess _highScoreDataAccess;

    private ILogger<GotchiServiceDataAccess> _logger;

    public GotchiServiceDataAccess(ILogger<GotchiServiceDataAccess> logger, IPersonDataAccess personDataAccess,
        IPortfolioDataAccess portfolioDataAccess,
        ICryptoCoinsDataAccess cryptoCoinsDataAccess,
        IGotchiDataAccess gotchiDataAccess,
        IHighScoreDataAccess highScoreDataAccess)
    {
        _logger = logger;
        _personDataAccess = personDataAccess;
        _portfolioDataAccess = portfolioDataAccess;
        _cryptoCoinsDataAccess = cryptoCoinsDataAccess;
        _gotchiDataAccess = gotchiDataAccess;
        _highScoreDataAccess = highScoreDataAccess;
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
    public async Task<PortfolioDTO?> PortfolioByIdAsync(string portfolioId) => await _portfolioDataAccess.PortfolioByIdAsync(portfolioId);
    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId) => _portfolioDataAccess.PortfoliosByPersonId(personId);
    public async Task<PortfolioDTO?> PortfolioByPersonIdAsync(string personId) => await _portfolioDataAccess.PortfolioByPersonIdAsync(personId);
    public ICollection<PortfolioDTO> PortfoliosAll() => _portfolioDataAccess.PortfoliosAll();

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId) => ICL(_cryptoCoinsDataAccess.CryptoCoinByCoinMarketId, coinMarketId, null!);
    public CryptoCoinDTO CryptoCoinByName(string name) => ICL(_cryptoCoinsDataAccess.CryptoCoinByName, name, null!);
    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug) => _cryptoCoinsDataAccess.CryptoCoinBySlug(slug);
    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol) => _cryptoCoinsDataAccess.CryptoCoinBySymbol(symbol);
    public async Task<CryptoCoinDTO?> CryptoCoinByCoinMarketIdAsync(string coinMarketId) => await _cryptoCoinsDataAccess.CryptoCoinByCoinMarketIdAsync(coinMarketId);
    public async Task<CryptoCoinDTO?> CryptoCoinByNameAsync(string name) => await _cryptoCoinsDataAccess.CryptoCoinByNameAsync(name);
    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySlugAsync(string slug) => await _cryptoCoinsDataAccess.CryptoCoinBySlugAsync(slug);
    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySymbolAsync(string symbol) => await _cryptoCoinsDataAccess.CryptoCoinBySymbolAsync(symbol);
    public async Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetNameSuggestionsAsync(prefix, limit);
    public async Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetSlugSuggestionsAsync(prefix, limit);
    public async Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetSymbolSuggestionsAsync(prefix, limit);


    public GotchiDTO GotchiById(string gotchiId) => ICL(_gotchiDataAccess.GotchiById, gotchiId, null!);
    public async Task<GotchiDTO?> GotchiByIdAsync(string gotchiId) => await _gotchiDataAccess.GotchiByIdAsync(gotchiId);
    public ICollection<GotchiDTO> GotchisByOwnerId(string ownerId) => _gotchiDataAccess.GotchisByOwnerId(ownerId);
    public ICollection<GotchiDTO> GotchisAll() => _gotchiDataAccess.GotchisAll();


    public async Task<ICollection<HighScoreDTO>> GetHighScoresAsync() => await _highScoreDataAccess.GetHighScoresAsync();

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
