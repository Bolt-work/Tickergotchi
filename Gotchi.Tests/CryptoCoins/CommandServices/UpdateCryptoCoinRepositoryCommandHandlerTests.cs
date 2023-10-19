using FluentAssertions;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.CryptoCoins.CommandServices;

public class UpdateCryptoCoinRepositoryCommandHandlerTests
{
    private MockCryptoCoinRepository _coinRepo;
    private CryptoCoinManger _cryptoCoinManger;
    private ILogger<UpdateCryptoCoinRepositoryCommandHandler> _logger;
    private UpdateCryptoCoinRepositoryCommandHandler _commandHandler;

    public UpdateCryptoCoinRepositoryCommandHandlerTests()
    {
        //Dependencies
        _coinRepo = CommandHandlerHelper.MockCryptoCoinRepository();
        _cryptoCoinManger = CommandHandlerHelper.CryptoCoinManger(_coinRepo);
        _logger = CommandHandlerHelper.Logger<UpdateCryptoCoinRepositoryCommandHandler>();

        _commandHandler = new UpdateCryptoCoinRepositoryCommandHandler(_cryptoCoinManger, _logger);
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
