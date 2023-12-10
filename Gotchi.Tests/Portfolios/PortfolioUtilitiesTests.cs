using FluentAssertions;
using Gotchi.Core.Helpers;
using Gotchi.Gotchis.Managers;
using Gotchi.Portfolios;
using Gotchi.Portfolios.Managers;

namespace Gotchi.Tests.Portfolios;

public class PortfolioUtilitiesTests
{
    [Fact]
    public void CalculatePortfolioBalance_ValidTwoHoursPassed_CorrectBalance()
    {
        float balance = 10_000F;
        DateTime now = DateTime.UtcNow;
        DateTime lastUpdated = now.Subtract(new TimeSpan(2,0,0));
        DateTime current = now;

        // Act
        var result = PortfolioManager.CalculatePortfolioBalance(balance, lastUpdated, current);

        // Assert
        var shouldBalance = balance;
        shouldBalance = shouldBalance - GameSettings.Values().DeductionBaseAmount;
        float deductionPercentage = (float)GameSettings.Values().DeductionPercentage / 100;
        shouldBalance -= (float)shouldBalance * deductionPercentage;

        shouldBalance = shouldBalance - GameSettings.Values().DeductionBaseAmount;
        deductionPercentage = (float)GameSettings.Values().DeductionPercentage / 100;
        shouldBalance -= (float)shouldBalance * deductionPercentage;

        result.Should().Be(shouldBalance);
    }

    [Fact]
    public void CalculatePortfolioBalance_BalanceEmpty_ZeroBalance()
    {
        float balance = 0F;
        DateTime now = DateTime.UtcNow;
        DateTime lastUpdated = now.Subtract(new TimeSpan(2, 0, 0));
        DateTime current = now;

        // Act
        var result = PortfolioManager.CalculatePortfolioBalance(balance, lastUpdated, current);

        // Assert
        result.Should().Be(0);
    }
}
