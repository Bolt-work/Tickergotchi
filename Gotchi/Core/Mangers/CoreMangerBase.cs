namespace Gotchi.Core.Mangers;

public class CoreMangerBase
{

    public T ThrowIfModelNull<T>(T model, string identifier) 
    {
        var checkObject = model ?? throw new ModelIsNullException<T>(identifier);
        return (T) checkObject;
    }

}
