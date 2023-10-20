using FluentAssertions;
using Gotchi.Core.Helpers;
using Gotchi.Core.Mangers;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Models;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Persons.CommandServices;

public class CreatePersonCommandHandlerTests
{
    private MockPersonRepository _personRepo;
    private PersonManger _personManger;
    private ILogger<CreatePersonCommandHandler> _logger;
    private CreatePersonCommandHandler _commandHandler;

    public CreatePersonCommandHandlerTests()
    {
        //Dependencies
        _personRepo = CommandHandlerHelper.MockPersonRepository();
        _personManger = CommandHandlerHelper.PersonManger(_personRepo);
        _logger = CommandHandlerHelper.Logger<CreatePersonCommandHandler>();
        _commandHandler = new CreatePersonCommandHandler(_personManger, _logger);
    }

    [Fact]
    public void Handle_ValidCreatePersonCommand_PersonIsCreated() 
    {
        // Arrange
        _personRepo.DeleteAll();
        var personId = CoreHelper.NewId();
        var command = new CreatePersonCommand(personId, "First", "Last");

        //Act
        _commandHandler.Handle(command);

        // Assert
        var person = _personRepo.GetById(personId);
        person.Should().NotBeNull();
        person.FirstName.Should().Be("First");
        person.LastName.Should().Be("Last");
    }

    [Fact]
    public void Handle_InvalidCreatePersonCommand_AlreadyExistsError()
    {
        // Arrange
        _personRepo.DeleteAll();
        _personRepo.AddTestPerson();
        var personId = _personRepo.TestPerson.Id;
        var command = new CreatePersonCommand(personId!, "First", "Last");

        //Act
        Action act = () => _commandHandler.Handle(command);

        // Assert
        act.Should().Throw<ModelWithIdAlreadyExistsException<Person>>();
    }
}
