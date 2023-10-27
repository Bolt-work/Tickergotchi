using Gotchi.Core.Services;
using Gotchi.Portfolios.Managers;
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

public class RemoveAssetCommandHandler : CoreCommandHandlerBase<RemoveAssetCommand>
{
    private IPortfolioManager _portfolioManager;

    public RemoveAssetCommandHandler(IPortfolioManager portfolioManager, ILogger<RemoveAssetCommandHandler> logger)
        : base(logger)
    {
        _portfolioManager = portfolioManager;
    }

    public override void Handle(RemoveAssetCommand command)
    {
        var portfolio = _portfolioManager.GetByPortfolioId(command.PortfolioId);
        _portfolioManager.RemovePortfolioAsset(portfolio, command.AssetCoinMarketId);
        _portfolioManager.Store(portfolio);
    }
}
