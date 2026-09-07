using System;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public class MonthlyRentalStrategy : IRentalCalculationStrategy
{
    public RentalCalculationResult Calculate(RentalContract contract, DateTime actualReturnDate, decimal returnKm)
    {
        var actualDuration = actualReturnDate - contract.Period.StartDate.Add(contract.Period.StartTime);
        var totalDays = (decimal)Math.Ceiling(actualDuration.TotalDays);
        if (totalDays <= 0) totalDays = 1;

        var totalMonths = Math.Ceiling(totalDays / 30m);

        var baseRent = totalMonths * contract.Pricing.RentPrice;
        var discount = totalMonths * contract.Pricing.DiscountAmount;

        decimal delayPenalty = 0;
        var expectedReturn = contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime);
        if (actualReturnDate > expectedReturn)
        {
            var delayHours = (actualReturnDate - expectedReturn).TotalHours;
            if (delayHours > (double)contract.Penalties.AllowedDelayHours)
            {
                delayPenalty = (decimal)Math.Ceiling(delayHours) * contract.Penalties.DelayPenaltyPerHour;
            }
        }

        var totalAmount = (baseRent - discount) + delayPenalty;

        return new RentalCalculationResult(baseRent, discount, delayPenalty, totalAmount);
    }
}
