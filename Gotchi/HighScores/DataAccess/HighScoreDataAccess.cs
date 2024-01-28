using AutoMapper;
using Gotchi.HighScores.DTOs;
using Gotchi.HighScores.Mangers;

namespace Gotchi.HighScores.DataAccess;

public class HighScoreDataAccess : IHighScoreDataAccess
{
    IDAHighScoreManager _highScoreManger;
    IMapper _mapper;
    public HighScoreDataAccess(IDAHighScoreManager highScoreManger, IMapper mapper)
    {
        _highScoreManger = highScoreManger;
        _mapper = mapper;
    }

    public async Task<ICollection<HighScoreDTO>> GetHighScoresAsync()
    {
        var gotchiModel = await _highScoreManger.GetHighScoresAsync();
        return _mapper.Map<ICollection<HighScoreDTO>>(gotchiModel);
    }
}
