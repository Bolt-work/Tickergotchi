using Gotchi.HighScores.DTOs;

namespace Gotchi.HighScores.DataAccess;

public interface IHighScoreDataAccess
{
    Task<ICollection<HighScoreDTO>> GetHighScoresAsync();
}
