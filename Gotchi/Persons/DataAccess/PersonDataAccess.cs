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

    public PersonDTO PersonById(string? id)
    {
        var personModel = _personManager.GetPersonById(id);
        return _mapper.Map<PersonDTO>(personModel);
    }

    public async Task<PersonDTO?> PersonByIdAsync(string? id)
    {
        var personModel = await _personManager.GetPersonByIdAsync(id);
        if (personModel is null)
            return null;
        return _mapper.Map<PersonDTO>(personModel);
    }

    public ICollection<PersonDTO> PersonsAll()
    {
        var personModel = _personManager.GetAllPersons();
        return _mapper.Map<ICollection<PersonDTO>>(personModel);
    }
}
