using Gotchi.Core.Managers;

namespace Gotchi.Authentications.Mangers
{

    public class UserNameAlreadyExistsException : CoreManagerException
    {
        public UserNameAlreadyExistsException(string? userName)
            : base(
                $"UserName: {userName} , already exists"
                )
        { }
    }

    public class PasswordIsNullOrWhitespaceException : CoreManagerException
    {
        public PasswordIsNullOrWhitespaceException()
            : base(
                $"Password is null or white space"
                )
        { }
    }

    public class UserNameIsNullOrWhitespaceException : CoreManagerException
    {
        public UserNameIsNullOrWhitespaceException()
            : base(
                $"Username is null or white space"
                )
        { }
    }
}
