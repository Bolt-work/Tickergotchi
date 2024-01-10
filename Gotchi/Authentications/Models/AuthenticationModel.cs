using Gotchi.Core.Models;

namespace Gotchi.Authentications.Models;

public class AuthenticationModel : CoreModelBase
{
    public string? PersonId { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
    public string? UserName { get; set; }
    public AuthenticationModel(string id)
    {
        Id = id;
    }
}
