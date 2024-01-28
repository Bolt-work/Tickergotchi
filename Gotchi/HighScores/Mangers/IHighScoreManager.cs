using Gotchi.Authentications.Models;
using Gotchi.Gotchis.Models;
using Gotchi.HighScores.Models;
using Gotchi.Persons.Models;

namespace Gotchi.HighScores.Mangers;

public interface IHighScoreManager
{
    HighScore AddHighScore(Person? user, AuthenticationModel? auth, CryptoGotchi? gotchi);
    bool Store(HighScore person);
}
