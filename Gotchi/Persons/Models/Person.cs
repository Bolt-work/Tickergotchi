using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gotchi.Core.Models;

namespace Gotchi.Persons.Models;

public class Person : CoreModelBase
{
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public Person(string id)
    {
        Id = id;
    }
}
