using Gotchi.Core.Helpers;

namespace Gotchi.Portfolios;

public static class PortfolioUtilities
{
    public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated) => CalculatePortfolioBalance(balance, balanceLastUpdated, DateTime.UtcNow);
    public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated, DateTime currentDataTime)
    {
        if (balance < 1)
            return 0;

        var hours = CoreHelper.NumberOfHoursPassed(balanceLastUpdated, currentDataTime);
        float newBalance = balance;
        for (int i = hours; i > 0; i--)
        {
            newBalance = newBalance - GameSettings.Values().DeductionBaseAmount;
            newBalance -= (float) newBalance * (GameSettings.Values().DeductionPercentage / 100);
        }

        return (newBalance > 0) ? newBalance : 0;
    }
}
