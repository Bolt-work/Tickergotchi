using Gotchi.Authentications.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Authentications.Mangers;

public interface IAuthenticationManger
{
    Task<AuthenticationModel?> AuthenticationByPasswordAndUserName(string? password, string? userName);
    AuthenticationModel CreateUserAuthentication(Person? person, string? password, string? userName);
    Task<bool> UserNameAlreadyExistAsync(string? userName);
}