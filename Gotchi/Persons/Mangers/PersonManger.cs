using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;
using System.Security.Cryptography;
using System.Text;

namespace Gotchi.Persons.Managers
{
    public class PersonManager : CoreManagerBase, IPersonManager
    {
        private IPersonRepository _personRepository;

        public PersonManager(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(string? userName, string? password) => Create(CoreHelper.NewId(), userName, password);

        public Person Create(string? personId, string? userName, string? password)
        {
            if (string.IsNullOrEmpty(personId)) 
                throw new ArgumentStringNullOrEmptyException(nameof(personId));
            
            if(_personRepository.ExistsById(personId))
                throw new ModelWithIdAlreadyExistsException<Person>(personId);

            var _userName = CheckAndCleanUserName(userName);
            if (DoesUserNameAlreadyExist(_userName))
                throw new UserNameAlreadyUsedExceptions(_userName);

            if (string.IsNullOrEmpty(password))
                throw new ArgumentStringNullOrEmptyException(nameof(password));

            var _password = GetHashString(password);

            return new Person(personId) 
            {
                UserName = userName,
                Password = _password,
                Role = "User"
            };
        }

        public Person GetPersonById(string? personId)
        {
            if(string.IsNullOrEmpty(personId))
                throw new ArgumentNullException(nameof(personId));
            
            var person = _personRepository.GetById(personId);
            return ThrowIfModelNotFound(person, personId);
        }

        public Person GetPersonByUserName(string? userName)
        {
            var _userName = CheckAndCleanUserName(userName);
            var person = _personRepository.GetByUserName(_userName);
            return ThrowIfModelNotFound(person, _userName);
        }

        public bool DoesUserNameAlreadyExist(string? userName) 
        {
            var _userName = CheckAndCleanUserName(userName);
            return _personRepository.ExistsByUserName(_userName);
        }

        public bool CheckPasswordWithUserName(string? password, string? userName) 
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentStringNullOrEmptyException(nameof(password));

            var _userName = CheckAndCleanUserName(userName);
            var person = _personRepository.GetByUserName(_userName);
            ThrowIfModelNotFound(person, _userName);

            return person.Password == GetHashString(password);
        }

        public bool CheckPasswordAndPersonId(string? password, string? personId)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentStringNullOrEmptyException(nameof(password));

            if (string.IsNullOrEmpty(personId))
                throw new ArgumentStringNullOrEmptyException(nameof(personId));

            var person = _personRepository.GetById(personId);
            ThrowIfModelNotFound(person, personId);

            return person.Password == GetHashString(password);
        }

        private string CheckAndCleanUserName(string? userName) 
        {
            var _userName = userName ?? throw new ArgumentNullException(nameof(userName));
            _userName = _userName.Trim();

            if (string.IsNullOrEmpty(_userName))
                throw new ArgumentStringNullOrEmptyException(nameof(_userName));

            return _userName;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _personRepository.GetAll();
        }

        public bool Delete(Person person)
        {
            return _personRepository.Delete(person);
        }

        public bool Store(Person person)
        {
            return _personRepository.Upsert(person);
        }

        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}
