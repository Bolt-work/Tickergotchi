
using Gotchi.Authentications.DataAccess;
using Gotchi.Authentications.Mangers;
using Gotchi.Authentications.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Authentications;

public class AuthenticationServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<AuthenticationRepositorySettings>();
        services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        services.AddSingleton<IAuthenticationManger, AuthenticationManger>();

        // Data Access
        services.AddSingleton<IAuthenticationDataAccess, AuthenticationDataAccess>();

        // Command Handlers

    }
}
