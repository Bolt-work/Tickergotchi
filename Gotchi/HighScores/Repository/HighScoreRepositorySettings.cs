using Gotchi.Core.Repository;

namespace Gotchi.HighScores.Repository;

public class HighScoreRepositorySettings : RepositorySettings
{
    public HighScoreRepositorySettings()
    {
        CollectionName = "HighScores";
    }
}