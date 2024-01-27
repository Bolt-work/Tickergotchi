using Gotchi.Persons.DTOs;

namespace Gotchi.Persons.DataAccess
{
    public interface IPersonDataAccess
    {
        PersonDTO PersonById(string? id);
        Task<PersonDTO?> PersonByIdAsync(string? id);
        ICollection<PersonDTO> PersonsAll();
    }
}