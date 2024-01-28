using Gotchi.Gotchis.DTOs;

namespace Gotchi.Gotchis.DataAccess
{
    public interface IGotchiDataAccess
    {
        Task<GotchiDTO?> GotchiByIdAsync(string gotchiId);
    }
}