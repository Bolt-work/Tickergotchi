using Gotchi.Gotchis.Models;

namespace Gotchi.Gotchis.Mangers;

public interface IDAGotchiManager
{
    Task<CryptoGotchi?> GetGotchiByIdAsync(string? gotchiId);
}
