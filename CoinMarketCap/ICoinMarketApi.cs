using CoinMarketCap.DataModels;

namespace CoinMarketCap
{
    public interface ICoinMarketApi
    {
        RootModel CallApi();
        RootModel CallTestApi();
    }
}