﻿using CoinMarketCap.DataModels;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace CoinMarketCap;

public class CoinMarketApi : ICoinMarketApi
{
    private string _testAddress = null!;
    private string _testApiKey = null!;

    private string _activeAddress = null!;
    private string _apiKey = null!;

    private HttpClient _httpClient;

    public CoinMarketApi(CoinMarketKeys apiKeys)
    {
        _testApiKey = apiKeys.TestAddress;
        _testApiKey = apiKeys.TestApiKey;

        _activeAddress = apiKeys.ActiveAddress;
        _apiKey = apiKeys.ApiKey;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public RootModel CallTestApi() => MakeAPICall(_testAddress);

    public RootModel CallApi() => MakeAPICall(_activeAddress);

    private RootModel MakeAPICall(string address)
    {
        var URL = new UriBuilder($"https://{address}/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "5000";
        queryString["convert"] = "USD";

        URL.Query = queryString.ToString();


        //var client = new WebClient();
        //client.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
        //client.Headers.Add("Accepts", "application/json");
        //var result = client.DownloadString(URL.ToString());

        var result = _httpClient.GetStringAsync(URL.ToString()).Result;
        ArgumentNullException.ThrowIfNull(result);
        var json = JsonSerializer.Deserialize<RootModel>(result);
        ArgumentNullException.ThrowIfNull(json);
        return json;
    }
}
