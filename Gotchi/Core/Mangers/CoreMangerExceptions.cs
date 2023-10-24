namespace Gotchi.Core.Managers;

public abstract class CoreManagerException : Exception 
{
    public CoreManagerException(string message) : base(message) { }
}

public class ModelIsNullException<T> : CoreManagerException
{
    public ModelIsNullException(string identifier) 
        : base($"Model is null, id : {identifier}, Type expected : {typeof(T).FullName}"){}
}

public class ModelWithIdAlreadyExistsException<T> : CoreManagerException
{
    public ModelWithIdAlreadyExistsException(string identifier)
        : base($"Model with id : {identifier}, Type {typeof(T).FullName}") { }
}
