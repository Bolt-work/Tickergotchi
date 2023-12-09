using Gotchi.Core.Repository;

namespace Gotchi.Authentications.Repository;

public class AuthenticationRepositorySettings : RepositorySettings
{
    public AuthenticationRepositorySettings()
    {
        CollectionName = "Authentication";
    }
}
