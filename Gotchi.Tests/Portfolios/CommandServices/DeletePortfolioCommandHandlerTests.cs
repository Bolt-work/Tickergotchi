using FluentAssertions;
using Gotchi.Core.Mangers;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.Mangers;
using Gotchi.Portfolios.Models;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Portfolios.CommandServices;

public class DeletePortfolioCommandHandlerTests
{
    private MockPortfolioRepository _portfolioRepo;
    private PortfolioManger _portfolioManger;

    private ILogger<DeletePortfolioCommandHandler> _logger;

    private DeletePortfolioCommandHandler _commandHandler;
    public DeletePortfolioCommandHandlerTests()
    {
        //Dependencies
        _logger = CommandHandlerHelper.Logger<DeletePortfolioCommandHandler>();

        _portfolioRepo = CommandHandlerHelper.MockPortfolioRepository();
        _portfolioManger = CommandHandlerHelper.PortfolioManger(_portfolioRepo);

        _commandHandler = new DeletePortfolioCommandHandler(_portfolioManger, _logger);
    }

    [Fact]
    public void Handle_ValidDeletePortfolioCommand_CreatesPortfolio()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        _portfolioRepo.AddTestPortfolio();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var command = new DeletePortfolioCommand(portfolioId!);

        //Act
        _commandHandler.Handle(command);

        // Assert
        _portfolioRepo.GetByPortfolioId(portfolioId!).Should().BeNull();
    }

    [Fact]
    public void Handle_InValidDeletePortfolioCommand_ModelNullException()
    {
        // Arrange
        _portfolioRepo.DeleteAll();
        var portfolioId = _portfolioRepo.TestPortfolio.Id;
        var command = new DeletePortfolioCommand(portfolioId!);

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ModelIsNullException<Portfolio>>();
    }
}
