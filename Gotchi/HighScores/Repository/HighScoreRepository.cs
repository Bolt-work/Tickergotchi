using Gotchi.Core.Repository;
using Gotchi.HighScores.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gotchi.HighScores.Repository;

public class HighScoreRepository : RepositoryBase<HighScore>, IHighScoreRepository
{
    public HighScoreRepository(HighScoreRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(HighScore model) => base.UpsertEntry(model);
    public bool Delete(string id) => base.DeleteEntry(id);
    public async Task<ICollection<HighScore>> GetAllHighScoresAsync() 
    {
        var sortDefinition = Builders<HighScore>.Sort.Ascending(doc => doc.Score);
        return await ConnectToMongo().Find(_ => true)
            .Sort(sortDefinition)
            .ToListAsync();
    }


}
