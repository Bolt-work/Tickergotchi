using AutoMapper;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Persons.DataAccess;

public class PersonDataAccess : IPersonDataAccess
{
    IPersonRepository _personRepository;
    IMapper _mapper;
    public PersonDataAccess(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public PersonDTO PersonById(string id)
    {
        var personModel = _personRepository.GetById(id);
        return _mapper.Map<PersonDTO>(personModel);
    }
}
