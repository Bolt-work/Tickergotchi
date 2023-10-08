using Gotchi.Models;

namespace Gotchi.Mangers
{
    public interface IPersonManger
    {
        Person Create(string id);
        bool Delete(Person person);
        Person Get(string id);
        bool Store(Person person);
    }
}