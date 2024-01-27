using Gotchi.Authentications.Models;

namespace Gotchi.Authentications.Repository;

public interface IAuthenticationRepository
{
    AuthenticationModel GetByPersonId(string personId);
    Task<AuthenticationModel?> GetByUserNameAsync(string? userName);
    bool Upsert(AuthenticationModel model);
    bool UserNameAlreadyExist(string? userName);
    Task<bool> UserNameAlreadyExistAsync(string? userName);
}
