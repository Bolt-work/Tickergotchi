using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Managers;
using Microsoft.Extensions.Logging;

namespace Gotchi.CryptoCoins.CommandServices;

public class UpdateCryptoCoinRepositoryCommand : ICoreCommand
{
}

public class UpdateCryptoCoinRepositoryCommandHandler : CoreCommandHandlerBase
{
    private ICryptoCoinManager _cryptoCoinManager;
    public UpdateCryptoCoinRepositoryCommandHandler(ICryptoCoinManager cryptoCoinManager, ILogger<UpdateCryptoCoinRepositoryCommandHandler> logger)
        : base(logger)
    {
        _cryptoCoinManager = cryptoCoinManager;
    }

    public void Handle(UpdateCryptoCoinRepositoryCommand command)
    {
        base.Handle(command);
        _cryptoCoinManager.UpdateCoinValues();
    }
}
