using AutoMapper;
using Gotchi.Authentications.DTOs;
using Gotchi.Authentications.Mangers;

namespace Gotchi.Authentications.DataAccess;

public class AuthenticationDataAccess : IAuthenticationDataAccess
{
    private readonly IAuthenticationManger _authenticationManger;
    private readonly IMapper _mapper;
    public AuthenticationDataAccess(IAuthenticationManger authenticationManger, IMapper mapper)
    {
        _authenticationManger = authenticationManger;
        _mapper = mapper;
    }

    public async Task<AuthenticationDTO?> AuthenticationByPasswordAndUserName(string? password, string? userName)
    {
        var authModel = await _authenticationManger.AuthenticationByPasswordAndUserName(password, userName);
        if (authModel is null)
            return null;

        return _mapper.Map<AuthenticationDTO>(authModel);

    }
    public async Task<bool> UserNameAlreadyExistAsync(string? userName) => await _authenticationManger.UserNameAlreadyExistAsync(userName);
}
