using Gotchi.Gotchis.DTOs;

namespace Gotchi.Gotchis.DataAccess
{
    public interface IGotchiDataAccess
    {
        GotchiDTO GotchiById(string gotchiId);
        ICollection<GotchiDTO> GotchisAll();
        ICollection<GotchiDTO> GotchisByOwnerId(string ownerId);
    }
}