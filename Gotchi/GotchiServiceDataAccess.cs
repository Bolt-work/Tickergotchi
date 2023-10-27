using Gotchi.CryptoCoins.DataAccess;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Gotchis.DataAccess;
using Gotchi.Gotchis.DTOs;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.DTOs;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.DTOs;

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

    public GotchiServiceDataAccess(IPersonDataAccess personDataAccess,
        IPortfolioDataAccess portfolioDataAccess,
        ICryptoCoinsDataAccess cryptoCoinsDataAccess,
        IGotchiDataAccess gotchiDataAccess)
    {
        _personDataAccess = personDataAccess;
        _portfolioDataAccess = portfolioDataAccess;
        _cryptoCoinsDataAccess = cryptoCoinsDataAccess;
        _gotchiDataAccess = gotchiDataAccess;
    }

    public PersonDTO PersonById(string id) => _personDataAccess.PersonById(id);
    public ICollection<PersonDTO> PersonsAll() => _personDataAccess.PersonsAll();

    public PortfolioDTO PortfolioById(string portfolioId) => _portfolioDataAccess.PortfolioById(portfolioId);
    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId) => _portfolioDataAccess.PortfoliosByPersonId(personId);
    public ICollection<PortfolioDTO> PortfoliosAll() => _portfolioDataAccess.PortfoliosAll();

    public CryptoCoinDTO CryptoCoinByCoinMarketId(string coinMarketId) => _cryptoCoinsDataAccess.CryptoCoinByCoinMarketId(coinMarketId);
    public CryptoCoinDTO CryptoCoinByName(string name) => _cryptoCoinsDataAccess.CryptoCoinByName(name);
    public ICollection<CryptoCoinDTO> CryptoCoinBySlug(string slug) => _cryptoCoinsDataAccess.CryptoCoinBySlug(slug);
    public ICollection<CryptoCoinDTO> CryptoCoinBySymbol(string symbol) => _cryptoCoinsDataAccess.CryptoCoinBySymbol(symbol);

    public GotchiDTO GotchiById(string gotchiId) => _gotchiDataAccess.GotchiById(gotchiId);
    public ICollection<GotchiDTO> GotchisByOwnerId(string ownerId) => _gotchiDataAccess.GotchisByOwnerId(ownerId);

}
