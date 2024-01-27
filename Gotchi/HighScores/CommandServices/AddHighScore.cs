using Gotchi.Authentications.Mangers;
using Gotchi.Core.Services;
using Gotchi.Gotchis.Managers;
using Gotchi.HighScores.Mangers;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;
using System.Net;

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
    private readonly IAuthenticationManger _authenticationManger;
    public AddHighScoreCommandHandler(IHighScoreManager highScoreManager, IGotchiManager gotchiManager, IPersonManager personManager, IAuthenticationManger authenticationManger, ILogger<AddHighScoreCommandHandler> logger)
        : base(logger)
    {
        _highScoreManager = highScoreManager;
        _gotchiManager = gotchiManager;
        _personManager = personManager;
        _authenticationManger = authenticationManger;
    }

    public override void Handle(AddHighScoreCommand command)
    {
        var person = _personManager.GetPersonById(command.PersonId);
        var gotchi = _gotchiManager.GetGotchiById(command.GotchiId);
        var auth = _authenticationManger.GetAuthenticationByPersonId(command.PersonId);
        var highScore = _highScoreManager.AddHighScore(person, auth, gotchi);
        _highScoreManager.Store(highScore);
    }
}
