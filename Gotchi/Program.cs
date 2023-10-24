
using CoinMarketCap;
using Gotchi.Core.Helpers;
using Gotchi.Core.Repository;
using Gotchi.Core.Services;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.Managers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Persons.CommandServices;
using Gotchi.Portfolios.CommandService;

namespace Gotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gotchiService = new GotchiHostService(args);
            var gotchiDataAccessService = gotchiService.DataAccess();
            var commandService = gotchiService.CommandService();

            //var personId = CoreHelper.NewId();
            var personId = "7294a4b7-362f-42d0-8780-5532bfba9001";
            var portfolioId = "7ae53002-0af1-470f-81b3-29e53d1896e5";
            //ICoreCommand c = new CreatePersonCommand(personId, "Doom", "Bolt");
            
            //ICoreCommand c = new CreatePortfolioCommand(personId, portfolioId);

            //ICoreCommand c = new UpdateCryptoCoinRepositoryCommand();

            ICoreCommand c = new BuyAssetsCommand(portfolioId, "1", 100);
            commandService.Process(c);

            //var portfolios = gotchiDataAccessService.PortfoliosByPersonId(personId);
            var portfolio = gotchiDataAccessService.PortfolioById(portfolioId);
            var t = 0;

            //"9f93b577-27ea-4d72-93c2-c99975b48217"
            //ICoreCommand h = new CreatePersonCommand(CoreHelper.NewId());
            //var n = h.GetType();

            //var t = 0;
        }
    }
}