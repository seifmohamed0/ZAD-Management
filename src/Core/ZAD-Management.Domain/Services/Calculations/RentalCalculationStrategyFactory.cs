using System;
using ZAD_Management.Domain.Enums;

namespace ZAD_Management.Domain.Services.Calculations;

public class RentalCalculationStrategyFactory
{
    public static IRentalCalculationStrategy GetStrategy(ContractType contractType)
    {
        return contractType switch
        {
            ContractType.Hourly => new HourlyRentalStrategy(),
            ContractType.Daily => new DailyRentalStrategy(),
            ContractType.Weekly => new WeeklyRentalStrategy(),
            ContractType.Monthly => new MonthlyRentalStrategy(),
            ContractType.Yearly => new YearlyRentalStrategy(),
            _ => throw new NotSupportedException($"Rental calculation for contract type {contractType} is not supported.")
        };
    }
}
