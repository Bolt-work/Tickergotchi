using Gotchi.Core.Managers;
using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.HighScores.Models;
using Gotchi.HighScores.Repository;
using Gotchi.Persons.Models;
using Microsoft.Extensions.Logging;

namespace Gotchi.HighScores.Mangers;

public class HighScoreManager : CoreManagerBase, IHighScoreManager
{
    private IHighScoreRepository _highScoreRepository;
    private ILogger _logger;
    public HighScoreManager(IHighScoreRepository highScoreRepository, ILogger<GotchiManager> logger)
    {
        _highScoreRepository = highScoreRepository;
        _logger = logger;
    }

    public HighScore AddHighScore(Person? user, CryptoGotchi? gotchi)
    {
        if (user is null)
            throw new ParameterModelIsNullException<Person>();

        if (gotchi is null)
            throw new ParameterModelIsNullException<CryptoGotchi>();

        if (gotchi.State != GotchiState.Dead)
            throw new GotchiIsNotDeadException(gotchi);

        return new HighScore
        {
            UserName = user.UserName,
            GotchiName = gotchi.Name,
            Score = gotchi.Level,
            DateSet = DateTime.UtcNow,
            TimeGotchiAlive = TimeAlive(gotchi)
        };
    }

    private TimeSpan TimeAlive(CryptoGotchi gotchi)
    {
        if (gotchi.DateOfDeath == DateTime.MinValue)
            return TimeSpan.Zero;

        return gotchi.DateOfDeath.Subtract(gotchi.Created);
    }

    #region Data Access 
    public Task<ICollection<HighScore>> GetHighScoresAsync() 
    {
        return _highScoreRepository.GetAllHighScoresAsync();
    }
    #endregion

    public bool Store(HighScore gotchi)
    {
        return _highScoreRepository.Upsert(gotchi);
    }
}
