using Gotchi.Authentications.Models;
using Gotchi.Core.Helpers;
using Gotchi.Core.Repository;

namespace Gotchi.Authentications.Repository;

public class AuthenticationRepository : RepositoryBase<AuthenticationModel>, IAuthenticationRepository
{
    public AuthenticationRepository(AuthenticationRepositorySettings settings)
        : base(settings)
    {
    }

    public bool Upsert(AuthenticationModel model) => base.UpsertEntry(model);
    public bool UserNameAlreadyExist(string? userName)
    {
        if(string.IsNullOrWhiteSpace(userName))
            return true;

       return base.EntryExistsByKey("UserName", userName);
    }

    public async Task<bool> UserNameAlreadyExistAsync(string? userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return true;

        return await base.EntryExistsByKeyAsync("UserName", userName);
    }

    public async Task<AuthenticationModel?> GetByUserNameAsync(string? userName) 
    {
        var _userName = CoreHelper.CleanUserName(userName);
        if(string.IsNullOrWhiteSpace(_userName))
            return null;

        return await base.GetByKeyStrAsync("UserName", _userName);
    } 

    public AuthenticationModel GetByPersonId(string personId) => base.GetByKeyStr("PersonId", personId);
}
