using Gotchi.CryptoCoins.DataAccess;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.DTOs;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.DTOs;

namespace Gotchi;

public class GotchiDataAccess: IPersonDataAccess, IPortfolioDataAccess, ICryptoCoinsDataAccess
{
    private IPersonDataAccess _personDataAccess;
    private IPortfolioDataAccess _portfolioDataAccess;
    private ICryptoCoinsDataAccess _cryptoCoinsDataAccess;
    public GotchiDataAccess(IPersonDataAccess personDataAccess, IPortfolioDataAccess portfolioDataAccess, ICryptoCoinsDataAccess cryptoCoinsDataAccess)
    {
        _personDataAccess = personDataAccess;
        _portfolioDataAccess = portfolioDataAccess;
        _cryptoCoinsDataAccess = cryptoCoinsDataAccess;
    }

    public PersonDTO PersonById(string id) => _personDataAccess.PersonById(id);

    public PortfolioDTO PortfolioById(string portfolioId) => _portfolioDataAccess.PortfolioById(portfolioId);
    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId) => _portfolioDataAccess.PortfoliosByPersonId(personId);

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId) => _cryptoCoinsDataAccess.CryptoCoinByCoinMarketId(coinMarketId);
    public CryptoCoinDTO CryptoCoinByName(string name) => _cryptoCoinsDataAccess.CryptoCoinByName(name);
    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug) => _cryptoCoinsDataAccess.CryptoCoinBySlug(slug);
    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol) => _cryptoCoinsDataAccess.CryptoCoinBySymbol(symbol);
}
