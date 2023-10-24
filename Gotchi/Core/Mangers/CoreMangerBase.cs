namespace Gotchi.Core.Managers;

public class CoreManagerBase
{

    public T ThrowIfModelNull<T>(T model, string identifier) 
    {
        var checkObject = model ?? throw new ModelIsNullException<T>(identifier);
        return (T) checkObject;
    }

}
