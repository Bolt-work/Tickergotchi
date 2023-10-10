using Gotchi.Core.Repository;

namespace Gotchi.CryptoCoins.Repository;

public class CryptoCoinRepositorySettings : RepositorySettings
{
    public CryptoCoinRepositorySettings()
    {
        CollectionName = "CryptoCoins";
    }
}