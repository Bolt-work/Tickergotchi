using AutoMapper;
using Gotchi.Gotchis.DTOs;
using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Mangers;

namespace Gotchi.Gotchis.DataAccess;

public class GotchiDataAccess : IGotchiDataAccess
{
    IDAGotchiManager _gotchiManager;
    IMapper _mapper;

    public GotchiDataAccess(IDAGotchiManager gotchiManager, IMapper mapper)
    {
        _gotchiManager = gotchiManager;
        _mapper = mapper;
    }

    public async Task<GotchiDTO?> GotchiByIdAsync(string gotchiId)
    {
        var gotchiModel = await _gotchiManager.GetGotchiByIdAsync(gotchiId);
        return _mapper.Map<GotchiDTO>(gotchiModel);
    }
}
