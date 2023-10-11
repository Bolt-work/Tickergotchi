using Gotchi.Core.Services;
using Gotchi.Persons.Mangers;

namespace Gotchi.Persons.CommandServices;

public class CreatePersonCommand : ICoreCommand
{
    public string PersonsId;

    public CreatePersonCommand(string personId)
    {
        PersonsId = personId;
    }
}

public class CreatePersonCommandHandler
{
    private IPersonManger _personManger;
    public CreatePersonCommandHandler(IPersonManger personManger)
    {
        _personManger = personManger;
    }

    public void Handle(CreatePersonCommand command) 
    {
        var person = _personManger.Create(command.PersonsId);
    }
}
