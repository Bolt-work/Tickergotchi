using CoinMarketCap.DataModels;
using System.Text.Json;

namespace CoinMarketCap
{
    public class Program
    {
        static void Main(string[] args)
        {
            var resultFilePath = "coinMarketResult.txt";
            Console.WriteLine("Hello, World!");
            //var result = CoinMarketCapApi.CallApi();
            //File.WriteAllText("coinMarketResult.txt", result);

            var resultJson = File.ReadAllText(resultFilePath);
            //var results = JsonSerializer.Deserialize<RootModel>(resultJson);
            var results = JsonSerializer.Deserialize<RootModel>(resultJson);
            var t = 0;
        }
    }
}