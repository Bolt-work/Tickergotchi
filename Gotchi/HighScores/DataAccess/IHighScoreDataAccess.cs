using Gotchi.HighScores.DTOs;

namespace Gotchi.HighScores.DataAccess;

public interface IHighScoreDataAccess
{
    Task<HighScoreDTO?> GetHighScoresAsync();
}
