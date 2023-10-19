using CoinMarketCap;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.Mangers;
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
    public static PersonManger PersonManger(IPersonRepository personRepository) => new PersonManger(personRepository);
    public static PersonManger PersonManger() => new PersonManger(MockPersonRepository());
    public static PersonManger PersonMangerWithData() => new PersonManger(MockPersonRepositoryWithData());


    public static MockCoinMarketApi CoinMarketApi() => new MockCoinMarketApi();
    public static MockCryptoCoinRepository MockCryptoCoinRepository() => new MockCryptoCoinRepository();
    public static MockCryptoCoinRepository MockCryptoCoinRepositoryWithData() => new MockCryptoCoinRepository(true);
    public static CryptoCoinManger CryptoCoinManger(ICryptoCoinRepository cryptoCoinRepository, ICoinMarketApi coinMarketApi) => new CryptoCoinManger(cryptoCoinRepository, coinMarketApi);
    public static CryptoCoinManger CryptoCoinManger(ICryptoCoinRepository cryptoCoinRepository) => new CryptoCoinManger(cryptoCoinRepository, CoinMarketApi());
    public static CryptoCoinManger CryptoCoinManger() => new CryptoCoinManger(MockCryptoCoinRepository(), CoinMarketApi());
    public static CryptoCoinManger CryptoCoinMangerWithData() => new CryptoCoinManger(MockCryptoCoinRepositoryWithData(), CoinMarketApi());


    public static MockPortfolioRepository MockPortfolioRepository() => new MockPortfolioRepository();
    public static MockPortfolioRepository MockPortfolioRepositoryWithData() => new MockPortfolioRepository(true);
    public static PortfolioManger PortfolioManger(IPortfolioRepository portfolioRepository) => new PortfolioManger(portfolioRepository);
    public static PortfolioManger PortfolioManger() => new PortfolioManger(MockPortfolioRepository());
    public static PortfolioManger PortfolioMangerWithData() => new PortfolioManger(MockPortfolioRepositoryWithData());
}
