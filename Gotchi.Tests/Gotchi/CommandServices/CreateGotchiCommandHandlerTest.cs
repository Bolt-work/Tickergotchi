using FluentAssertions;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.CommandServices;
using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Persons.Managers;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.Models;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Gotchi.CommandServices;

public class CreateGotchiCommandHandlerTest
{
    private MockGotchiRepository _gotchiRepo;
    private GotchiManager _gotchiManager;

    private MockPersonRepository _personRepository;
    private PersonManager _personManager;
    private ILogger<CreateGotchiCommandHandler> _logger;

    private CreateGotchiCommandHandler _commandHandler;

    public CreateGotchiCommandHandlerTest()
    {
        //Dependencies
        _personRepository = CommandHandlerHelper.MockPersonRepositoryWithData();
        _personManager = CommandHandlerHelper.PersonManager(_personRepository);
        _logger = CommandHandlerHelper.Logger<CreateGotchiCommandHandler>();

        _gotchiRepo = CommandHandlerHelper.MockGotchiRepository();
        _gotchiManager = CommandHandlerHelper.GotchiManager(_gotchiRepo);

        _commandHandler = new CreateGotchiCommandHandler(_gotchiManager, _personManager, _logger);
    }

    [Fact]
    public void Handle_ValidCreateGotchiCommand_CreatesGotchi()
    {
        // Arrange
        _gotchiRepo.DeleteAll();
        var personId = _personRepository.TestPerson.Id;
        var gotchiId = "gotchiIdTestId";
        var testName = "TestName";
        var command = new CreateGotchiCommand(personId!, testName, gotchiId);

        //Act
        _commandHandler.Handle(command);

        // Assert
        _gotchiRepo.GetAll().Should().NotBeEmpty();
        _gotchiRepo.GotchiByGotchiId(gotchiId).Should().NotBeNull();
        _gotchiRepo.GotchiByGotchiId(gotchiId).Name = testName;
    }

    [Fact]
    public void Handle_InvalidCreateGotchiCommand_AlreadyExistsError()
    {
        // Arrange
        _gotchiRepo.DeleteAll();
        _gotchiRepo.AddTestGotchi();
        var personId = _personRepository.TestPerson.Id;
        var gotchiId = _gotchiRepo.TestGotchi.Id;
        var testName = "TestName";
        var command = new CreateGotchiCommand(personId!, testName, gotchiId);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ModelWithIdAlreadyExistsException<CryptoGotchi>>();
    }
}
