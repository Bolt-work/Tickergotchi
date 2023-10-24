using Gotchi.Core.Managers;

namespace Gotchi.CryptoCoins.Managers;

public class CoinMarketApiModelPropertyIsNullExceptions : CoreManagerException
{
    public CoinMarketApiModelPropertyIsNullExceptions(object obj, string propertyName) 
        :base($"CoinMarketApiModel, {obj.GetType().FullName} property : {propertyName} is null"){ }
}
