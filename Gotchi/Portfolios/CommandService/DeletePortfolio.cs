using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Portfolios.Mangers;
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

public class DeletePortfolioCommandHandler : CoreCommandHandlerBase
{
    private IPortfolioManger _portfolioManger;

    public DeletePortfolioCommandHandler(IPortfolioManger portfolioManger, ILogger<DeletePortfolioCommandHandler> logger)
        : base(logger)
    {
        _portfolioManger = portfolioManger;
    }

    public void Handle(BuyAssetsCommand command)
    {
        base.Handle(command);
        var portfolio = _portfolioManger.GetByPortfolioId(command.PortfolioId);
        _portfolioManger.DeletePortfolio(portfolio);
    }
}
