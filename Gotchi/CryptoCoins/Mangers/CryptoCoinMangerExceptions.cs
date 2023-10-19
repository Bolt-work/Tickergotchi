using Gotchi.Core.Mangers;

namespace Gotchi.CryptoCoins.Mangers;

public class CoinMarketApiModelPropertyIsNullExceptions : CoreMangerException
{
    public CoinMarketApiModelPropertyIsNullExceptions(object obj, string propertyName) 
        :base($"CoinMarketApiModel, {obj.GetType().FullName} property : {propertyName} is null"){ }
}
