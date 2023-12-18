using CoinMarketCap;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.Managers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.Managers;
using Gotchi.Portfolios.Repository;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Gotchi.Tests;

public static class CommandHandlerHelper
{
    public static ILogger<T> Logger<T>()
    {
        return new NullLogger<T>();
    }

    public static MockPersonRepository MockPersonRepository() => new MockPersonRepository();
    public static MockPersonRepository MockPersonRepositoryWithData() => new MockPersonRepository(true);
    public static PersonManager PersonManager(IPersonRepository personRepository) => new PersonManager(personRepository);
    public static PersonManager PersonManager() => new PersonManager(MockPersonRepository());
    public static PersonManager PersonManagerWithData() => new PersonManager(MockPersonRepositoryWithData());


    public static MockCoinMarketApi CoinMarketApi() => new MockCoinMarketApi();
    public static MockCryptoCoinRepository MockCryptoCoinRepository() => new MockCryptoCoinRepository();
    public static MockCryptoCoinRepository MockCryptoCoinRepositoryWithData() => new MockCryptoCoinRepository(true);
    public static CryptoCoinManager CryptoCoinManager(ICryptoCoinRepository cryptoCoinRepository, ICoinMarketApi coinMarketApi) => new CryptoCoinManager(cryptoCoinRepository, coinMarketApi);
    public static CryptoCoinManager CryptoCoinManager(ICryptoCoinRepository cryptoCoinRepository) => new CryptoCoinManager(cryptoCoinRepository, CoinMarketApi());
    public static CryptoCoinManager CryptoCoinManager() => new CryptoCoinManager(MockCryptoCoinRepository(), CoinMarketApi());
    public static CryptoCoinManager CryptoCoinManagerWithData() => new CryptoCoinManager(MockCryptoCoinRepositoryWithData(), CoinMarketApi());


    public static MockPortfolioRepository MockPortfolioRepository() => new MockPortfolioRepository();
    public static MockPortfolioRepository MockPortfolioRepositoryWithData() => new MockPortfolioRepository(true);
    public static PortfolioManager PortfolioManager(IPortfolioRepository portfolioRepository) => new PortfolioManager(portfolioRepository, CryptoCoinManagerWithData(), Logger<PortfolioManager>());
    public static PortfolioManager PortfolioManager() => new PortfolioManager(MockPortfolioRepository(), CryptoCoinManagerWithData(), Logger<PortfolioManager>());
    public static PortfolioManager PortfolioManagerWithData() => new PortfolioManager(MockPortfolioRepositoryWithData(), CryptoCoinManagerWithData(), Logger<PortfolioManager>());

    public static MockGotchiRepository MockGotchiRepository() => new MockGotchiRepository();
    public static MockGotchiRepository MockGotchiRepositoryWithData() => new MockGotchiRepository(true);
    public static GotchiManager GotchiManager(IGotchiRepository portfolioRepository) => new GotchiManager(portfolioRepository);
    public static GotchiManager GotchiManager() => new GotchiManager(MockGotchiRepository());
    public static GotchiManager GotchiManagerWithData() => new GotchiManager(MockGotchiRepositoryWithData());
}
