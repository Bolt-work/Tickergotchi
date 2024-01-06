using Gotchi.Core.Services;
using Gotchi.Gotchis.Managers;
using Gotchi.HighScores.Mangers;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.HighScores.CommandServices;

public class AddHighScoreCommand : ICoreCommand
{
    public readonly string? PersonId;
    public readonly string? GotchiId;

    public AddHighScoreCommand(string personId, string gotchiId)
    {
        PersonId = personId;
        GotchiId = gotchiId;
    }
}

public class AddHighScoreCommandHandler : CoreCommandHandlerBase<AddHighScoreCommand>
{
    private readonly IHighScoreManager _highScoreManager;
    private readonly IGotchiManager _gotchiManager;
    private readonly IPersonManager _personManager;
    public AddHighScoreCommandHandler(IHighScoreManager highScoreManager, IGotchiManager gotchiManager, IPersonManager personManager, ILogger<AddHighScoreCommandHandler> logger)
        : base(logger)
    {
        _highScoreManager = highScoreManager;
        _gotchiManager = gotchiManager;
        _personManager = personManager;
    }

    public override void Handle(AddHighScoreCommand command)
    {
        var person = _personManager.GetPersonById(command.PersonId);
        var gotchi = _gotchiManager.GetGotchiById(command.GotchiId);
        var highScore = _highScoreManager.AddHighScore(person, gotchi);
        _highScoreManager.Store(highScore);
    }
}
