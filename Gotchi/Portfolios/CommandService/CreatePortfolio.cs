
using Gotchi.Core.Services;
using Gotchi.Persons.Managers;
using Gotchi.Portfolios.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.CommandService;

public class CreatePortfolioCommand : ICoreCommand
{
    public readonly string PersonsId;
    public string? PortfolioId;
    public CreatePortfolioCommand(string personId, string? portfolioId = null)
    {
        PersonsId = personId;
        PortfolioId = portfolioId;
    }
}


public class CreatePortfolioCommandHandler : CoreCommandHandlerBase
{
    private IPortfolioManager _portfolioManager;
    private IPersonManager _personManager;
    public CreatePortfolioCommandHandler(IPortfolioManager portfolioManager, IPersonManager personManager, ILogger<CreatePortfolioCommandHandler> logger)
        : base(logger)
    {
        _portfolioManager = portfolioManager;
        _personManager = personManager;
    }

    public void Handle(CreatePortfolioCommand command)
    {
        base.Handle(command);
        var accountHolder = _personManager.GetPersonById(command.PersonsId);
        var portfolio = _portfolioManager.CreatePortfolio(accountHolder, command.PortfolioId);
        _portfolioManager.Store(portfolio);
    }
}
