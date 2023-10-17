
using Gotchi.Core.Services;
using Gotchi.Persons.Mangers;
using Gotchi.Portfolios.Mangers;
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
    private IPortfolioManger _portfolioManger;
    private IPersonManger _personManger;
    public CreatePortfolioCommandHandler(IPortfolioManger portfolioManger, IPersonManger personManger, ILogger<CreatePortfolioCommandHandler> logger)
        : base(logger)
    {
        _portfolioManger = portfolioManger;
        _personManger = personManger;
    }

    public void Handle(CreatePortfolioCommand command)
    {
        base.Handle(command);
        var accountHolder = _personManger.GetById(command.PersonsId);
        var portfolio = _portfolioManger.CreatePortfolio(accountHolder, command.PortfolioId);
        _portfolioManger.Store(portfolio);
    }
}
