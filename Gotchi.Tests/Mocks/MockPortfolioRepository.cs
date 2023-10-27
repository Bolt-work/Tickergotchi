using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;

namespace Gotchi.Tests.Mocks;

public class MockPortfolioRepository : MockRepositoryBase<Portfolio> , IPortfolioRepository
{
    public Portfolio TestPortfolio;
    public Asset TestAsset;
    public MockPortfolioRepository(bool withTestData = false)
    {
        var accountHolder = CommandHandlerHelper.MockPersonRepository().TestPerson;
        TestPortfolio = new Portfolio("testPortfolioId", accountHolder)
        {
            BalanceLastUpdated = DateTime.UtcNow,
            Balance = 8_000F
        };

        TestAsset = new Asset()
        {
            Id = "testAssetId",
            CoinMarketId = "1",
            Name = "Bitcoin",
            Slug = "bitcoin",
            Symbol = "BTC",
            PriceWhenLastBought = 1000F,
            Units = 2000,
            Profit = 0 - 2000F,
        };

        if (withTestData)
            AddTestPortfolioAndAsset();
    }

    public void AddTestPortfolio() 
    {
        _db.Add(TestPortfolio);
    }

    public void AddTestPortfolioAndAsset()
    {
        TestPortfolio.Assets.Add(TestAsset);
        _db.Add(TestPortfolio);
    }

    public bool Delete(Portfolio portfolio) => base.DeleteEntry(portfolio);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public bool Exists(string id) => base.EntryExists(id);
    public IEnumerable<Portfolio> GetAll() => base.GetAllEntries();

    public IEnumerable<Portfolio> GetByPersonId(string personId) 
    {
        return _db.Where(x => x.AccountHolder.Id == personId).ToList();
    }

    public Portfolio GetByPortfolioId(string portfolioId)
    {
        return _db.SingleOrDefault(x => x.Id == portfolioId)!;
    }

    public bool Upsert(Portfolio model) => base.UpsertEntry(model);
}
