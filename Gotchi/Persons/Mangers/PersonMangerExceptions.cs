using Gotchi.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Persons.Mangers;

public class UserNameAlreadyUsedExceptions : CoreManagerException
{
    public UserNameAlreadyUsedExceptions(string userName) 
        :base($"User name {userName} already exists")
    {}
}
