using Gotchi.Core.Services;
using Gotchi.Persons.Mangers;
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


public class CreatePersonCommandHandler : CoreCommandHandlerBase
{
    private IPersonManger _personManger;
    public CreatePersonCommandHandler(IPersonManger personManger, ILogger<CreatePersonCommandHandler> logger)
        :base(logger)
    {
        _personManger = personManger;
    }

    public void Handle(CreatePersonCommand command) 
    {
        base.Handle(command);
        var person = _personManger.Create(command.PersonsId, command.FirstName, command.LastName);
        _personManger.Store(person);
    }
}
