using Gotchi.HighScores.Models;

namespace Gotchi.HighScores.Mangers;

public interface IDAHighScoreManager
{
    Task<ICollection<HighScore>> GetHighScoresAsync();
}
