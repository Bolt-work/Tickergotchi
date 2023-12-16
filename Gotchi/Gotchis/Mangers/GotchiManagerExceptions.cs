using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;
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

public class GotchiIsFullException : CoreManagerException
{
    public GotchiIsFullException(CryptoGotchi gotchi)
    : base(
        $"Gotchi is full, Id : {gotchi.Id}, Name : {gotchi.Name}"
        )
    { }
}

public class PersonAlreadyHasActiveGotchiException : CoreManagerException
{
    public PersonAlreadyHasActiveGotchiException(Person person)
    : base(
        $"Person with id {person.Id} already has active Gotchi"
        )
    { }
}