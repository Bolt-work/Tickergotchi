using Gotchi.Core.Models;

namespace Gotchi.Persons.Models;

public class Person : CoreModelBase
{
    public string? ActivePortfolio { get; set; }
    public string? ActiveGotchi{ get; set; }

    public Person(string id)
    {
        Id = id;
    }
}
