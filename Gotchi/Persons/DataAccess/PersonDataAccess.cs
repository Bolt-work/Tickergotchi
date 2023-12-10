using AutoMapper;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Managers;

namespace Gotchi.Persons.DataAccess;

public class PersonDataAccess : IPersonDataAccess
{
    IPersonManager _personManager;
    IMapper _mapper;
    public PersonDataAccess(IPersonManager personManager, IMapper mapper)
    {
        _personManager = personManager;
        _mapper = mapper;
    }

    public PersonDTO PersonById(string id)
    {
        var personModel = _personManager.GetPersonById(id);
        return _mapper.Map<PersonDTO>(personModel);
    }

    public PersonDTO PersonByUserName(string userName)
    {
        var personModel = _personManager.GetPersonByUserName(userName);
        return _mapper.Map<PersonDTO>(personModel);
    }

    public ICollection<PersonDTO> PersonsAll()
    {
        var personModel = _personManager.GetAllPersons();
        return _mapper.Map<ICollection<PersonDTO>>(personModel);
    }

    public bool CheckPasswordAndUserName(string? password, string userName) => _personManager.CheckPasswordWithUserName(password, userName);
    public bool CheckPasswordAndPersonId(string? password, string personId) => _personManager.CheckPasswordAndPersonId(password, personId);
    public bool DoesUserNameAlreadyExist(string? userName) => _personManager.DoesUserNameAlreadyExist(userName);
}
