using Gotchi.Persons.DTOs;

namespace Gotchi.Persons.DataAccess
{
    public interface IPersonDataAccess
    {
        bool CheckPasswordAndPersonId(string? password, string personId);
        bool CheckPasswordAndUserName(string? password, string userName);
        bool DoesUserNameAlreadyExist(string? userName);
        PersonDTO PersonById(string id);
        PersonDTO PersonByUserName(string userName);
        ICollection<PersonDTO> PersonsAll();
    }
}