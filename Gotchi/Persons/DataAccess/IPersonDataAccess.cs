using Gotchi.Persons.DTOs;

namespace Gotchi.Persons.DataAccess
{
    public interface IPersonDataAccess
    {
        Task<PersonDTO?> PersonByIdAsync(string? id);
    }
}