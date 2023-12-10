using Gotchi.Core.Services;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;

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
    public CreatePersonCommandHandler(IPersonManager personManager, ILogger<CreatePersonCommandHandler> logger)
        :base(logger)
    {
        _personManager = personManager;
    }

    public override void Handle(CreatePersonCommand command) 
    {
        var person = _personManager.Create(command.PersonsId, command.UserName, command.Password);
        _personManager.Store(person);
    }
}
