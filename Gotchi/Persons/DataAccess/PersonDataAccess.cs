using AutoMapper;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Mangers;

namespace Gotchi.Persons.DataAccess;

public class PersonDataAccess : IPersonDataAccess
{
    IDAPersonManager _personManager;
    IMapper _mapper;
    public PersonDataAccess(IDAPersonManager personManager, IMapper mapper)
    {
        _personManager = personManager;
        _mapper = mapper;
    }

    public async Task<PersonDTO?> PersonByIdAsync(string? id)
    {
        var personModel = await _personManager.GetPersonByIdAsync(id);
        if (personModel is null)
            return null;
        return _mapper.Map<PersonDTO>(personModel);
    }
}
