using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Mangers;
using Microsoft.Extensions.Logging;

namespace Gotchi.CryptoCoins.CommandServices;

public class UpdateCryptoCoinRepositoryCommand : ICoreCommand
{
}

public class UpdateCryptoCoinRepositoryCommandHandler : CoreCommandHandlerBase
{
    private ICryptoCoinManger _cryptoCoinManger;
    public UpdateCryptoCoinRepositoryCommandHandler(ICryptoCoinManger cryptoCoinManger, ILogger<CreatePersonCommandHandler> logger)
        : base(logger)
    {
        _cryptoCoinManger = cryptoCoinManger;
    }

    public void Handle(UpdateCryptoCoinRepositoryCommand command)
    {
        base.Handle(command);
        _cryptoCoinManger.UpdateCoinValues();
    }
}
