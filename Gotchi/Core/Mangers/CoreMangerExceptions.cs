namespace Gotchi.Core.Mangers;

public abstract class CoreMangerException : Exception 
{
    public CoreMangerException(string message) : base(message) { }
}

public class ModelIsNullException<T> : CoreMangerException
{
    public ModelIsNullException(string identifier) 
        : base($"Model is null, id : {identifier}, Type expected : {typeof(T).FullName}"){}
}

public class ModelWithIdAlreadyExistsException<T> : CoreMangerException
{
    public ModelWithIdAlreadyExistsException(string identifier)
        : base($"Model with id : {identifier}, Type {typeof(T).FullName}") { }
}
