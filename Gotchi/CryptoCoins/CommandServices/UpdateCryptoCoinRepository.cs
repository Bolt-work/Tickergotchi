using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.CryptoCoins.CommandServices;

public class UpdateCryptoCoinRepositoryCommand : ICoreCommand
{
}

public class UpdateCryptoCoinRepositoryCommandHandler : CoreCommandHandlerBase<UpdateCryptoCoinRepositoryCommand>
{
    private ICryptoCoinManager _cryptoCoinManager;
    public UpdateCryptoCoinRepositoryCommandHandler(ICryptoCoinManager cryptoCoinManager, ILogger<UpdateCryptoCoinRepositoryCommandHandler> logger)
        : base(logger)
    {
        _cryptoCoinManager = cryptoCoinManager;
    }

    public override void Handle(UpdateCryptoCoinRepositoryCommand command)
    {
        _cryptoCoinManager.UpdateCoinValues();
    }
}
