
using CoinMarketCap;
using Gotchi.Core.Helpers;
using Gotchi.Core.Repository;
using Gotchi.Core.Services;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var person = new Person(Guid.NewGuid().ToString());

            //var personSettings = new PersonRepositorySettings();
            //var personDb = new PersonRepository(personSettings);
            //personDb.Upsert(person);

            var a = GameSettings.Values();
            //var repo = new CryptoCoinRepository(new CryptoCoinRepositorySettings());
            //var coinManger = new CryptoCoinManger(repo, new CoinMarketApi());
            //coinManger.UpdateCoinValues();

            var coin = new CryptoCoin() 
            {
                Id = "Test"
            };

            //PersonRepositorySettings repoSettings = new();
            //PersonRepository personRepository = new(repoSettings);

            //Person person = new(CoreHelper.NewId());

            //personRepository.Upsert(person);

            ICoreCommand c = new CreatePersonCommand(CoreHelper.NewId());
            CommandService commandService = new CommandService();
            commandService.Start(args, c);

            //ICoreCommand h = new CreatePersonCommand(CoreHelper.NewId());
            //var n = h.GetType();

            //var t = 0;
        }
    }
}