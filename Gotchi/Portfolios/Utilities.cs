using Gotchi.Core.Helpers;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios;

public static class Utilities
{
    public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated) => CalculatePortfolioBalance(balance, balanceLastUpdated, DateTime.Now);
    public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated, DateTime currentDataTime)
    {
        if (balance < 1)
            return 0;

        var hours = CoreHelper.NumberOfHoursPassed(balanceLastUpdated, currentDataTime);
        var newBalance = balance;
        for (int i = hours; i > 0; i--)
        {
            newBalance = balance - GameSettings.Values().DeductionBaseAmount;
            newBalance -= newBalance * (GameSettings.Values().DeductionPercentage / 100);
        }

        return (newBalance > 0) ? newBalance : 0;
    }
}
