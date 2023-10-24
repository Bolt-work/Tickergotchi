using FluentAssertions;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.CryptoCoins.CommandServices;

public class UpdateCryptoCoinRepositoryCommandHandlerTests
{
    private MockCryptoCoinRepository _coinRepo;
    private CryptoCoinManager _cryptoCoinManager;
    private ILogger<UpdateCryptoCoinRepositoryCommandHandler> _logger;
    private UpdateCryptoCoinRepositoryCommandHandler _commandHandler;

    public UpdateCryptoCoinRepositoryCommandHandlerTests()
    {
        //Dependencies
        _coinRepo = CommandHandlerHelper.MockCryptoCoinRepository();
        _cryptoCoinManager = CommandHandlerHelper.CryptoCoinManager(_coinRepo);
        _logger = CommandHandlerHelper.Logger<UpdateCryptoCoinRepositoryCommandHandler>();

        _commandHandler = new UpdateCryptoCoinRepositoryCommandHandler(_cryptoCoinManager, _logger);
    }

    [Fact]
    public void Handle_UpdateCryptoCoinRepositoryCommand_DbIsUpdated()
    {
        // Arrange
        var command = new UpdateCryptoCoinRepositoryCommand();

        //Act
        _commandHandler.Handle(command);

        // Assert
        _coinRepo.GetAll().Should().NotBeEmpty();
    }
}
