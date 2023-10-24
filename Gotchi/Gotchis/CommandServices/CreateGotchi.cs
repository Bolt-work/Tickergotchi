using Gotchi.Core.Services;
using Gotchi.Gotchis.Managers;
using Gotchi.Persons.Managers;
using Gotchi.Portfolios.CommandService;
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
    private readonly IPersonManager _personManager;
    public CreateGotchiCommandHandler(IGotchiManager gotchiManager, IPersonManager personManager, ILogger<CreateGotchiCommandHandler> logger) 
        :base(logger)
    {
        _gotchiManager = gotchiManager;
        _personManager = personManager;
    }

    public void Handle(CreatePortfolioCommand command)
    {
        base.Handle(command);
        //var accountHolder = _personManager.GetPersonById(command.PersonsId);
        //var portfolio = _portfolioManager.CreatePortfolio(accountHolder, command.PortfolioId);
        //_portfolioManager.Store(portfolio);
    }
}
