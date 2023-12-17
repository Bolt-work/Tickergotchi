using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.Models;
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

        public Person Create(string? personId, string? userName, string? password)
        {
            if (string.IsNullOrEmpty(personId))
                personId = CoreHelper.NewId();

            if (_personRepository.ExistsById(personId))
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
                Role = "User",
                ActivePortfolio = null,
                ActiveGotchi = null,
            };
        }

        public Person SetPersonActivePortfolio(Person person, Portfolio portfolio) 
        {
            person.ActivePortfolio = portfolio.Id;
            return person;
        }

        public Person ClearPersonActivePortfolio(Person person)
        {
            person.ActivePortfolio = null;
            return person;
        }

        public Person SetPersonActiveGotchi(Person person, CryptoGotchi gotchi)
        {
            person.ActiveGotchi = gotchi.Id;
            return person;
        }

        public Person ClearPersonActiveGotchi(Person person)
        {
            person.ActiveGotchi = null;
            return person;
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

        private bool DoesUserNameAlreadyExist(string? userName)
        {
            var _userName = CheckAndCleanUserName(userName);
            return _personRepository.ExistsByUserName(_userName);
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

        #region Data Access

        public async Task<Person?> GetPersonByIdAsync(string? personId)
        {
            if (string.IsNullOrEmpty(personId))
                return null;

            return await _personRepository.GetByIdAsync(personId);
        }

        public async Task<Person?> GetPersonByUserNameAsync(string? userName)
        {
            var _userName = CleanUserName(userName);
            if(string.IsNullOrEmpty(_userName))
                return null;

            return await _personRepository.GetByUserNameAsync(_userName);
        }

        public async Task<bool> CheckPasswordWithUserName(string? password, string? userName)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            var _userName = CleanUserName(userName);
            if (string.IsNullOrEmpty(_userName))
                return false;

            var person = await _personRepository.GetByUserNameAsync(_userName);
            if(person is null)
                return false;

            return person.Password == GetHashString(password);
        }

        public async Task<bool> CheckPasswordAndPersonId(string? password, string? personId)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (string.IsNullOrEmpty(personId))
                return false;

            var person = await _personRepository.GetByIdAsync(personId);
            if(person is null)
                return false;

            return person.Password == GetHashString(password);
        }

        public async Task<bool> DoesUserNameAlreadyExistAsync(string? userName)
        {
            if (string.IsNullOrEmpty(userName))
                return true;

            var _userName = CleanUserName(userName);
            if (string.IsNullOrEmpty(_userName))
                return true;

            return await _personRepository.ExistsByUserNameAsync(_userName);
        }

        #endregion

        private string CheckAndCleanUserName(string? userName)
        {
            var _userName = userName ?? throw new ArgumentNullException(nameof(userName));
            _userName = CleanUserName(_userName);

            if (string.IsNullOrEmpty(_userName))
                throw new ArgumentStringNullOrEmptyException(nameof(_userName));

            return _userName;
        }

        private string? CleanUserName(string? userName) 
        {
            if (string.IsNullOrEmpty(userName))
                return "";

            return userName.Trim();
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
