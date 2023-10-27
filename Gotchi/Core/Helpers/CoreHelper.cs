namespace Gotchi.Core.Helpers;

public static class CoreHelper
{
    public static string NewId() => Guid.NewGuid().ToString();

    public static int NumberOfHoursPassed(DateTime since) => NumberOfHoursPassed(since, DateTime.UtcNow);
    public static int NumberOfHoursPassed(DateTime since, DateTime until) 
    {
        var hours = until.Subtract(since).Hours;
        return (hours <= 0) ? 0 : hours;
    }

}
