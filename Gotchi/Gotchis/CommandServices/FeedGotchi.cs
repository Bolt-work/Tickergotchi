using Gotchi.Core.Services;
using Gotchi.Gotchis.Managers;
using Gotchi.Portfolios.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Gotchis.CommandServices;

public class FeedGotchiCommand : ICoreCommand
{
    public readonly string? GotchiId;
    public readonly string? PortfolioId;
    public readonly int FoodAmount;

    public FeedGotchiCommand(string? gotchiId, string? portfolioId)
    {
        GotchiId = gotchiId;
        PortfolioId = portfolioId;
    }
}

public class FeedGotchiCommandHandler : CoreCommandHandlerBase<FeedGotchiCommand>
{
    private readonly IGotchiManager _gotchiManager;
    private readonly IPortfolioManager _portfolioManager;
    public FeedGotchiCommandHandler(IGotchiManager gotchiManager, IPortfolioManager portfolioManager, ILogger<FeedGotchiCommandHandler> logger)
        : base(logger)
    {
        _gotchiManager = gotchiManager;
        _portfolioManager = portfolioManager;
    }

    public override void Handle(FeedGotchiCommand command)
    {
        var gotchi = _gotchiManager.GetGotchiById(command.GotchiId);
        var portfolio = _portfolioManager.GetByPortfolioId(command.PortfolioId);

        _portfolioManager.WithdrawFromAccount(portfolio, gotchi.PriceForFood);
        _gotchiManager.FeedGotchi(gotchi);

        _gotchiManager.Store(gotchi);
        _portfolioManager.Store(portfolio);
    }
}
