using CoinMarketCap.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace CoinMarketCap;

public class CoinMarketApi : ICoinMarketApi
{
    private static string _testAddress = "sandbox-api.coinmarketcap.com";
    private static string _testApiKey = "b54bcf4d-1bca-4e8e-9a24-22ff2c3d462c";

    private static string _activeAddress = "pro-api.coinmarketcap.com";
    private static string _apiKey = "2c2da0e7-48ba-4850-824e-c5f8eb50de48";

    public RootModel CallTestApi() => MakeAPICall(_testAddress, _testApiKey);

    public RootModel CallApi() => MakeAPICall(_activeAddress, _apiKey);

    private RootModel MakeAPICall(string address, string apiKey)
    {
        // pro-api.coinmarketcap.com
        //var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
        var URL = new UriBuilder($"https://{address}/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "5000";
        queryString["convert"] = "USD";

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
        client.Headers.Add("Accepts", "application/json");
        var result = client.DownloadString(URL.ToString());
        return JsonSerializer.Deserialize<RootModel>(result);
    }
}
