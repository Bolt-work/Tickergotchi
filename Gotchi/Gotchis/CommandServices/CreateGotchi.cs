using Gotchi.Core.Services;
using Gotchi.Gotchis.Mangers;
using Gotchi.Persons.Mangers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Gotchis.CommandServices;

public class CreateGotchiCommand : ICoreCommand
{
    public readonly string? PersonId;
    public readonly string? Name;
    public readonly string? GotchiId;

    public CreateGotchiCommand(string personId, string name, string? gotchiId = null)
    {
        PersonId = personId;
        Name = name;
        GotchiId = gotchiId;
    }
}

public class CreateGotchiCommandHandler: CoreCommandHandlerBase
{
    private readonly IGotchiManager _gotchiManager;
    public CreateGotchiCommandHandler(IGotchiManager gotchiManager, ILogger<CreateGotchiCommandHandler> logger) 
        :base(logger)
    {
        _gotchiManager = gotchiManager;
    }

    public 
}
