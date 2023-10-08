using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Models;

public class Person : ModelBase
{
    public string? Name { get; set; }

    public Person(string id)
    {
        Id = id;
    }
}
