using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Gotchis.Mangers;

public class GotchiIsDeadException : CoreManagerException
{
    public GotchiIsDeadException(CryptoGotchi gotchi)
        : base(
            $"Gotchi is dead, Id : {gotchi.Id}, Name : {gotchi.Name}"
            )
    { }
}