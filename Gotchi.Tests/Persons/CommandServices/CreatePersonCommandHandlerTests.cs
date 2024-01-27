using FluentAssertions;
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Models;
using Gotchi.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace Gotchi.Tests.Persons.CommandServices;

public class CreatePersonCommandHandlerTests
{
    private MockPersonRepository _personRepo;
    private PersonManager _personManager;
    private ILogger<CreatePersonCommandHandler> _logger;
    //private CreatePersonCommandHandler _commandHandler;

    private string _passwordHash = "EB970EB0951C6CDEAC1EC0CC723FC91E30B0C26EE6F3B5EE0E574DB7F487DC55";

    public CreatePersonCommandHandlerTests()
    {
        //Dependencies
        _personRepo = CommandHandlerHelper.MockPersonRepository();
        _personManager = CommandHandlerHelper.PersonManager(_personRepo);
        _logger = CommandHandlerHelper.Logger<CreatePersonCommandHandler>();
        //_commandHandler = new CreatePersonCommandHandler(_personManager, _logger);
    }

    [Fact]
    public void Handle_ValidCreatePersonCommand_PersonIsCreated() 
    {
        // Arrange
        _personRepo.DeleteAll();
        var personId = CoreHelper.NewId();
        var command = new CreatePersonCommand(personId, "userName", "Last");

        //Act
        //_commandHandler.Handle(command);

        // Assert
        var person = _personRepo.GetById(personId);
        person.Should().NotBeNull();
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
        //Action act = () => _commandHandler.Handle(command);

        // Assert
        //act.Should().Throw<ModelWithIdAlreadyExistsException<Person>>();
    }
}
