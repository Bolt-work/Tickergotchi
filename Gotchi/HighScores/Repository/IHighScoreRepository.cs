using Gotchi.HighScores.Models;

namespace Gotchi.HighScores.Repository;

public interface IHighScoreRepository
{
    bool Delete(string id);
    Task<ICollection<HighScore>> GetAllHighScoresAsync();
    bool Upsert(HighScore model);
}
