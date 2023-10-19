using CoinMarketCap;
using CoinMarketCap.DataModels;

namespace Gotchi.Tests.Mocks;

public class MockCoinMarketApi : ICoinMarketApi
{
    public RootModel CallApi()
    {
        return new RootModel
        {
            data = new Datum[] 
            {
                new Datum 
                {
                    id = 1,
                    name = "Bitcoin",
                    symbol = "BTC",
                    slug = "bitcoin",
                    last_updated = DateTime.Now,

                    quote = new Quote
                    {
                        USD = new USD { price = 27126.615234375F }
                    }
                }
            }

        };
    }

    public RootModel CallTestApi() => CallApi();
}
