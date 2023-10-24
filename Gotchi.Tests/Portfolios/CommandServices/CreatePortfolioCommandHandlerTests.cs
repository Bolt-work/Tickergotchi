using FluentAssertions;
using Gotchi.Core.Managers;
using Gotchi.Persons.Managers;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.Managers;
using Gotchi.Portfolios.Models;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Portfolios.CommandServices;

public class CreatePortfolioCommandHandlerTests
{
    private MockPortfolioRepository _portfolioRepo;
    private PortfolioManager _portfolioManager;

    private MockPersonRepository _personRepository;
    private PersonManager _personManager;
    private ILogger<CreatePortfolioCommandHandler> _logger;
    private CreatePortfolioCommandHandler _commandHandler;

    public CreatePortfolioCommandHandlerTests()
    {
        //Dependencies
        _personRepository = CommandHandlerHelper.MockPersonRepositoryWithData();
        _personManager = CommandHandlerHelper.PersonManager(_personRepository);

        _logger = CommandHandlerHelper.Logger<CreatePortfolioCommandHandler>();

        _portfolioRepo = CommandHandlerHelper.MockPortfolioRepository();
        _portfolioManager = CommandHandlerHelper.PortfolioManager(_portfolioRepo);

        _commandHandler = new CreatePortfolioCommandHandler(_portfolioManager, _personManager, _logger);
    }

    [Fact]
    public void Handle_ValidCreatePortfolioCommand_CreatesPortfolio()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        var personId = _personRepository.TestPerson.Id;
        var portfolioId = "portfolioUnitTestId";
        var command = new CreatePortfolioCommand(personId!, portfolioId);

        //Act
        _commandHandler.Handle(command);

        // Assert
        _portfolioRepo.GetAll().Should().NotBeEmpty();
        _portfolioRepo.GetByPersonId(personId!).Should().NotBeEmpty();
        _portfolioRepo.GetByPortfolioId(portfolioId).Should().NotBeNull();
    }

    [Fact]
    public void Handle_InvalidCreatePortfolioCommand_AlreadyExistsError()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var personId = _personRepository.TestPerson.Id;
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var command = new CreatePortfolioCommand(personId!, portfolioId);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ModelWithIdAlreadyExistsException<Portfolio>>();
    }
}
