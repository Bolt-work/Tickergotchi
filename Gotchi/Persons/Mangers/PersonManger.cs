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
            var _id = personId ?? throw new ArgumentNullException(nameof(personId));
            _id = _id.Trim();

            if (string.IsNullOrEmpty(_id)) 
                throw new ArgumentStringNullOrEmpty(nameof(personId));
            
            if(_personRepository.ExistsById(_id))
                throw new ModelWithIdAlreadyExistsException<Person>(_id);

            
            var _userName = userName ?? throw new ArgumentNullException(nameof(userName));
            _userName = _userName.Trim();

            if (string.IsNullOrEmpty(_userName))
                throw new ArgumentStringNullOrEmpty(nameof(userName));
            
            if (_personRepository.ExistsByUserName(_userName))
                throw new UserNameAlreadyUsedExceptions(_userName);

            
            var _password = password ?? throw new ArgumentNullException(nameof(password));

            return new Person(_id) 
            {
                UserName = userName,
                Password = GetHashString(_password),
            };
        }

        public Person GetPersonById(string? personId)
        {
            var id = personId ?? throw new ArgumentNullException(nameof(personId)); 
            var person = _personRepository.GetById(id);
            return ThrowIfModelNotFound(person, id);
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
