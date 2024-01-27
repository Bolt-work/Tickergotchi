using Gotchi.Authentications.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Authentications.Mangers;

public interface IAuthenticationManger
{
    Task<AuthenticationModel?> AuthenticationByPasswordAndUserName(string? password, string? userName);
    AuthenticationModel CreateUserAuthentication(Person? person, string? password, string? userName);
    AuthenticationModel GetAuthenticationByPersonId(string? personId);
    Task<bool> UserNameAlreadyExistAsync(string? userName);
    bool Store(AuthenticationModel auth);
}