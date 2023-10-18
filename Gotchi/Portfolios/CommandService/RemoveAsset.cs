using Gotchi.Core.Services;
using Gotchi.Portfolios.Mangers;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.CommandService;

public class RemoveAssetCommand : ICoreCommand
{
    public readonly string PortfolioId;
    public readonly string AssetCoinMarketId;

    public RemoveAssetCommand(string portfolioId, string assetCoinMarketId)
    {
        PortfolioId = portfolioId;
        AssetCoinMarketId = assetCoinMarketId;
    }
}

public class RemoveAssetCommandHandler : CoreCommandHandlerBase
{
    private IPortfolioManger _portfolioManger;

    public RemoveAssetCommandHandler(IPortfolioManger portfolioManger, ILogger<RemoveAssetCommandHandler> logger)
        : base(logger)
    {
        _portfolioManger = portfolioManger;
    }

    public void Handle(RemoveAssetCommand command)
    {
        base.Handle(command);
        var portfolio = _portfolioManger.GetByPortfolioId(command.PortfolioId);
        _portfolioManger.RemovePortfolioAsset(portfolio, command.AssetCoinMarketId);
        _portfolioManger.Store(portfolio);
    }
}
