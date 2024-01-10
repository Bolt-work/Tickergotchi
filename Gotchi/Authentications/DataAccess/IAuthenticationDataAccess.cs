using Gotchi.Authentications.DTOs;

namespace Gotchi.Authentications.DataAccess;

public interface IAuthenticationDataAccess
{
    Task<AuthenticationDTO?> AuthenticationByPasswordAndUserName(string? password, string? userName);
    Task<bool> UserNameAlreadyExistAsync(string? userName);
}