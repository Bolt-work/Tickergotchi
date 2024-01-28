using Gotchi.Authentications.DataAccess;
using Gotchi.Authentications.DTOs;
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
    private IAuthenticationDataAccess _authenticationDataAccess;

    private ILogger<GotchiServiceDataAccess> _logger;

    public GotchiServiceDataAccess(ILogger<GotchiServiceDataAccess> logger, IPersonDataAccess personDataAccess,
        IPortfolioDataAccess portfolioDataAccess,
        ICryptoCoinsDataAccess cryptoCoinsDataAccess,
        IGotchiDataAccess gotchiDataAccess,
        IHighScoreDataAccess highScoreDataAccess,
        IAuthenticationDataAccess authenticationDataAccess)
    {
        _logger = logger;
        _personDataAccess = personDataAccess;
        _portfolioDataAccess = portfolioDataAccess;
        _cryptoCoinsDataAccess = cryptoCoinsDataAccess;
        _gotchiDataAccess = gotchiDataAccess;
        _highScoreDataAccess = highScoreDataAccess;
        _authenticationDataAccess = authenticationDataAccess;
    }

    public async Task<PersonDTO?> PersonByIdAsync(string? id) => await _personDataAccess.PersonByIdAsync(id);


    public async Task<PortfolioDTO?> PortfolioByIdAsync(string portfolioId) => await _portfolioDataAccess.PortfolioByIdAsync(portfolioId);
    public async Task<PortfolioDTO?> PortfolioByPersonIdAsync(string personId) => await _portfolioDataAccess.PortfolioByPersonIdAsync(personId);


    public async Task<CryptoCoinDTO?> CryptoCoinByCoinMarketIdAsync(string coinMarketId) => await _cryptoCoinsDataAccess.CryptoCoinByCoinMarketIdAsync(coinMarketId);
    public async Task<CryptoCoinDTO?> CryptoCoinByNameAsync(string name) => await _cryptoCoinsDataAccess.CryptoCoinByNameAsync(name);
    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySlugAsync(string slug) => await _cryptoCoinsDataAccess.CryptoCoinBySlugAsync(slug);
    public async Task<ICollection<CryptoCoinDTO>> CryptoCoinBySymbolAsync(string symbol) => await _cryptoCoinsDataAccess.CryptoCoinBySymbolAsync(symbol);
    public async Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetNameSuggestionsAsync(prefix, limit);
    public async Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetSlugSuggestionsAsync(prefix, limit);
    public async Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20) => await _cryptoCoinsDataAccess.GetSymbolSuggestionsAsync(prefix, limit);


    public async Task<GotchiDTO?> GotchiByIdAsync(string gotchiId) => await _gotchiDataAccess.GotchiByIdAsync(gotchiId);


    public async Task<ICollection<HighScoreDTO>> GetHighScoresAsync() => await _highScoreDataAccess.GetHighScoresAsync();

    public async Task<AuthenticationDTO?> AuthenticationByPasswordAndUserName(string? password, string? userName) => await _authenticationDataAccess.AuthenticationByPasswordAndUserName(password, userName);
    public async Task<bool> UserNameAlreadyExistAsync(string? userName) => await _authenticationDataAccess.UserNameAlreadyExistAsync(userName);
}
