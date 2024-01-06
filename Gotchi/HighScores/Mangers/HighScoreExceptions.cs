using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;

namespace Gotchi.HighScores.Mangers;

public class GotchiIsNotDeadException : CoreManagerException
{
    public GotchiIsNotDeadException(CryptoGotchi gotchi)
    : base(
        $"Gotchi is not dead, with id {gotchi.Id}"
        )
    { }
}