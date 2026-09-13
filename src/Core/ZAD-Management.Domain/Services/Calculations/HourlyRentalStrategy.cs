using System;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public class HourlyRentalStrategy : IRentalCalculationStrategy
{
    public RentalCalculationResult Calculate(RentalContract contract, DateTime actualReturnDate, decimal returnKm)
    {
        var actualDuration = actualReturnDate - contract.Period.StartDate.Add(contract.Period.StartTime);
        var totalHours = (decimal)Math.Ceiling(actualDuration.TotalHours);
        if (totalHours <= 0) totalHours = 1;

        var baseRent = totalHours * contract.Pricing.RentPrice;
        var discount = totalHours * contract.Pricing.DiscountAmount;

        var expectedReturn = contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime);
        var delayPenalty = RentalPenaltyCalculator.CalculateDelayPenalty(
            expectedReturn, actualReturnDate, contract.Penalties);

        var totalAmount = (baseRent - discount) + delayPenalty;

        return new RentalCalculationResult(baseRent, discount, delayPenalty, totalAmount, totalHours / 24m);
    }
}
