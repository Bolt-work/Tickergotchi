using Gotchi.Core.Repository;

namespace Gotchi.Portfolios.Repository;

public class PortfolioRepositorySettings : RepositorySettings
{
    public PortfolioRepositorySettings()
    {
        CollectionName = "Portfolios";
    }
}
