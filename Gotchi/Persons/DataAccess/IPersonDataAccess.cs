using Gotchi.Persons.DTOs;

namespace Gotchi.Persons.DataAccess
{
    public interface IPersonDataAccess
    {
        Task<bool> CheckPasswordAndPersonIdAsync(string? password, string? personId);
        Task<bool> CheckPasswordAndUserNameAsync(string? password, string? userName);
        Task<bool> DoesUserNameAlreadyExistAsync(string? userName);
        PersonDTO PersonById(string? id);
        Task<PersonDTO?> PersonByIdAsync(string? id);
        PersonDTO PersonByUserName(string? userName);
        Task<PersonDTO?> PersonByUserNameAsync(string? userName);
        ICollection<PersonDTO> PersonsAll();
    }
}