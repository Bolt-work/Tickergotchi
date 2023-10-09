using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Models;

public class CryptoGotchi : GotchiBase
{
    public CryptoGotchi(string id, Person owner) : base(id, owner)
    {

    }
}
