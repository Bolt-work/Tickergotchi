using Gotchi.Core.Services;
using Gotchi.Persons.Mangers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Gotchis.CommandServices;

public class CreateGotchiCommand : ICoreCommand
{
    public readonly string? PersonId;
    public readonly string? GotchiId;

    public CreateGotchiCommand(string personId, string? gotchiId = null)
    {
        PersonId = personId;
        GotchiId = gotchiId;
    }
}

public class CreateGotchiCommandHandler: CoreCommandHandlerBase
{
    private readonly IPersonManger _personManger;
    public CreateGotchiCommandHandler(IPersonManger personManger, ILogger<CreateGotchiCommandHandler> logger) 
        :base(logger)
    {
        
    }
}
