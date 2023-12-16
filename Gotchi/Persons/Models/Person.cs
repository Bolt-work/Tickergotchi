using Gotchi.Core.Models;

namespace Gotchi.Persons.Models;

public class Person : CoreModelBase
{
    public string? UserName { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }

    public string? ActivePortfolio { get; set; }
    public string? ActiveGotchi{ get; set; }

    public Person(string id)
    {
        Id = id;
    }
}
