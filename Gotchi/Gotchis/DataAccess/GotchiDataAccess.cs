using AutoMapper;
using Gotchi.Gotchis.DTOs;
using Gotchi.Gotchis.Managers;

namespace Gotchi.Gotchis.DataAccess;

public class GotchiDataAccess : IGotchiDataAccess
{
    IGotchiManager _gotchiManager;
    IMapper _mapper;

    public GotchiDataAccess(IGotchiManager gotchiManager, IMapper mapper)
    {
        _gotchiManager = gotchiManager;
        _mapper = mapper;
    }

    public GotchiDTO GotchiById(string gotchiId)
    {
        var gotchiModel = _gotchiManager.GetGotchiById(gotchiId);
        return _mapper.Map<GotchiDTO>(gotchiModel);
    }

    public async Task<GotchiDTO?> GotchiByIdAsync(string gotchiId)
    {
        var gotchiModel = await _gotchiManager.GetGotchiByIdAsync(gotchiId);
        return _mapper.Map<GotchiDTO>(gotchiModel);
    }

    public ICollection<GotchiDTO> GotchisByOwnerId(string ownerId)
    {
        var gotchiModels = _gotchiManager.GetGotchisByOwnerId(ownerId);
        return _mapper.Map<ICollection<GotchiDTO>>(gotchiModels);
    }

    public ICollection<GotchiDTO> GotchisAll()
    {
        var gotchiModels = _gotchiManager.GotchisAll();
        return _mapper.Map<ICollection<GotchiDTO>>(gotchiModels);
    }
}
