using Gotchi.Gotchis.Models;
using Gotchi.HighScores.Models;
using Gotchi.Persons.Models;

namespace Gotchi.HighScores.Mangers;

public interface IHighScoreManager
{
    HighScore AddHighScore(Person? user, CryptoGotchi? gotchi);
    Task<ICollection<HighScore>> GetHighScoresAsync();
    bool Store(HighScore person);
}
