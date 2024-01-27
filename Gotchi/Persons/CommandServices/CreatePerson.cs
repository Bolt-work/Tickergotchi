using Gotchi.Authentications.Mangers;
using Gotchi.Core.Services;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Gotchi.Persons.CommandServices;

public class CreatePersonCommand : ICoreCommand
{
    public string? PersonsId;
    public string? UserName;
    public string? Password;
    public CreatePersonCommand(string? userName, string? password, string? personId = null) 
    {
        PersonsId = personId;
        UserName = userName;
        Password = password;
    }
}


public class CreatePersonCommandHandler : CoreCommandHandlerBase<CreatePersonCommand>
{
    private IPersonManager _personManager;
    private IAuthenticationManger _authenticationManger;

    public CreatePersonCommandHandler(IPersonManager personManager, IAuthenticationManger authenticationManger, ILogger<CreatePersonCommandHandler> logger)
        :base(logger)
    {
        _personManager = personManager;
        _authenticationManger = authenticationManger;
    }

    public override void Handle(CreatePersonCommand command) 
    {
        var person = _personManager.Create(command.PersonsId);
        var auth = _authenticationManger.CreateUserAuthentication(person, command.Password, command.UserName);
        _personManager.Store(person);
        _authenticationManger.Store(auth);
    }
}
