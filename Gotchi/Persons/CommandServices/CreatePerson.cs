using Gotchi.Core.Services;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Persons.CommandServices;

public class CreatePersonCommand : ICoreCommand
{
    public string PersonsId;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public CreatePersonCommand(string personId, string? firstName = null, string? lastName = null) 
    {
        PersonsId = personId;
        FirstName = firstName;
        LastName = lastName;
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
        var person = _personManager.Create(command.PersonsId, command.FirstName, command.LastName);
        _personManager.Store(person);
    }
}
