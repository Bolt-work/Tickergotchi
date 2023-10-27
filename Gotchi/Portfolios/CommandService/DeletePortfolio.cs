using Gotchi.Core.Services;
using Gotchi.Portfolios.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.CommandService;

public class DeletePortfolioCommand : ICoreCommand
{
    public readonly string PortfolioId;
    public DeletePortfolioCommand(string portfolioId)
    {
        PortfolioId = portfolioId;
    }
}

public class DeletePortfolioCommandHandler : CoreCommandHandlerBase<DeletePortfolioCommand>
{
    private IPortfolioManager _portfolioManager;

    public DeletePortfolioCommandHandler(IPortfolioManager portfolioManager, ILogger<DeletePortfolioCommandHandler> logger)
        : base(logger)
    {
        _portfolioManager = portfolioManager;
    }

    public override void Handle(DeletePortfolioCommand command)
    {
        var portfolio = _portfolioManager.GetByPortfolioId(command.PortfolioId);
        _portfolioManager.DeletePortfolio(portfolio);
    }
}
