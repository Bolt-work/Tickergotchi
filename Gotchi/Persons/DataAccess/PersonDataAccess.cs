using AutoMapper;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Mangers;

namespace Gotchi.Persons.DataAccess;

public class PersonDataAccess : IPersonDataAccess
{
    IPersonManger _personManger;
    IMapper _mapper;
    public PersonDataAccess(IPersonManger personManger, IMapper mapper)
    {
        _personManger = personManger;
        _mapper = mapper;
    }

    public PersonDTO PersonById(string id)
    {
        var personModel = _personManger.GetPersonById(id);
        return _mapper.Map<PersonDTO>(personModel);
    }
}
