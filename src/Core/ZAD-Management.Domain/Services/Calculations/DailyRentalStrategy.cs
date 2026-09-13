using System;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public class DailyRentalStrategy : IRentalCalculationStrategy
{
    public RentalCalculationResult Calculate(RentalContract contract, DateTime actualReturnDate, decimal returnKm)
    {
        var actualDuration = actualReturnDate - contract.Period.StartDate.Add(contract.Period.StartTime);
        var totalDays = (decimal)Math.Ceiling(actualDuration.TotalDays);
        if (totalDays <= 0) totalDays = 1;

        var baseRent = totalDays * contract.Pricing.RentPrice;
        var discount = totalDays * contract.Pricing.DiscountAmount;

        var expectedReturn = contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime);
        var delayPenalty = RentalPenaltyCalculator.CalculateDelayPenalty(
            expectedReturn, actualReturnDate, contract.Penalties);

        var totalAmount = (baseRent - discount) + delayPenalty;

        return new RentalCalculationResult(baseRent, discount, delayPenalty, totalAmount, totalDays);
    }
}
