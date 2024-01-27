using Gotchi.Authentications.Models;
using Gotchi.Authentications.Repository;
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Persons.Models;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Gotchi.Authentications.Mangers;

public class AuthenticationManger : CoreManagerBase, IAuthenticationManger
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly ILogger _logger;
    public AuthenticationManger(IAuthenticationRepository authenticationRepository, ILogger<AuthenticationManger> logger)
    {
        _authenticationRepository = authenticationRepository;
        _logger = logger;
    }
    public AuthenticationModel CreateUserAuthentication(Person? person, string? password, string? userName)
    {
        if (person is null)
            throw new ParameterModelIsNullException<Person>();

        if (string.IsNullOrWhiteSpace(password))
            throw new PasswordIsNullOrWhitespaceException();

        var _userName = CoreHelper.CleanUserName(userName);
        if (string.IsNullOrWhiteSpace(_userName))
            throw new UserNameIsNullOrWhitespaceException();

        if (_authenticationRepository.UserNameAlreadyExist(_userName))
            throw new UserNameAlreadyExistsException(_userName);

        return new AuthenticationModel(CoreHelper.NewId())
        {
            PersonId = person.Id,
            Role = "User",
            Password = GetHashString(password),
            UserName = _userName,
        };
    }

    public async Task<bool> UserNameAlreadyExistAsync(string? userName)
    {
        return await _authenticationRepository.UserNameAlreadyExistAsync(userName);
    }

    public async Task<AuthenticationModel?> AuthenticationByPasswordAndUserName(string? password, string? userName) 
    {
        if (string.IsNullOrWhiteSpace(password))
            return null;

        var _userName = CoreHelper.CleanUserName(userName);
        if (string.IsNullOrWhiteSpace(_userName))
            return null;

        var authModel = await _authenticationRepository.GetByUserNameAsync(_userName);
        if (authModel is null)
            return null;

        if (authModel.Password != GetHashString(password))
            return null;

        return authModel;
    }

    private static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    public bool Store(AuthenticationModel auth) 
    {
        if (auth is null)
            throw new ParameterModelIsNullException<AuthenticationModel>();

        return _authenticationRepository.Upsert(auth);
    }

    public AuthenticationModel GetAuthenticationByPersonId(string? personId) 
    {
        if (string.IsNullOrWhiteSpace(personId))
            throw new ArgumentStringNullOrEmptyException("PersonId");

        var auth = _authenticationRepository.GetByPersonId(personId);

        if (auth is null)
            throw new ModelNotFoundException<AuthenticationModel>(personId);

        return auth;
    }
}
