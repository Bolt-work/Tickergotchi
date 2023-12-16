using Gotchi.Core.Services;
using Gotchi.Gotchis.Managers;
using Gotchi.Persons.Managers;
using Gotchi.Portfolios.CommandService;
using Microsoft.Extensions.Logging;

namespace Gotchi.Gotchis.CommandServices;

public class CreateGotchiCommand : ICoreCommand
{
    public readonly string? OwnerId;
    public readonly string? Name;
    public readonly string? GotchiId;

    public CreateGotchiCommand(string ownerId, string name, string? gotchiId = null)
    {
        OwnerId = ownerId;
        Name = name;
        GotchiId = gotchiId;
    }
}

public class CreateGotchiCommandHandler: CoreCommandHandlerBase<CreateGotchiCommand>
{
    private readonly IGotchiManager _gotchiManager;
    private readonly IPersonManager _personManager;
    public CreateGotchiCommandHandler(IGotchiManager gotchiManager, IPersonManager personManager, ILogger<CreateGotchiCommandHandler> logger) 
        :base(logger)
    {
        _gotchiManager = gotchiManager;
        _personManager = personManager;
    }

    public override void Handle(CreateGotchiCommand command)
    {
        var owner = _personManager.GetPersonById(command.OwnerId);
        var gotchi = _gotchiManager.CreateCryptoGotchi(owner, command.Name, command.GotchiId);
        var updatedOwner = _personManager.SetPersonActiveGotchi(owner, gotchi);
        _gotchiManager.Store(gotchi);
        _personManager.Store(updatedOwner);
    }
}
