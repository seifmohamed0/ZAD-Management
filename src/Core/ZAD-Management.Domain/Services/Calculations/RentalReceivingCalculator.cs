using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public static class RentalReceivingCalculator
{
    public static RentalReceivingResult Calculate(
        RentalContract contract,
        DateTime receivingDate,
        decimal receivingKilometerCounter)
    {
        var start = contract.Period.StartDate.Add(contract.Period.StartTime);
        var expected = contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime);
        var actualPeriodInDays = Math.Max(1m, (decimal)Math.Ceiling((receivingDate - start).TotalDays));
        var delayHours = RentalPenaltyCalculator.GetBillableDelayHours(
            expected, receivingDate, contract.Penalties);
        var totalConsumption = Math.Max(0m, receivingKilometerCounter - contract.Vehicle.KilometerCounter);
        var freeKilometers = Math.Max(0m, contract.Vehicle.MaximumKilometerPerDay * actualPeriodInDays);
        var exceededKilometers = Math.Max(0m, totalConsumption - freeKilometers);
        var exceededAmount = exceededKilometers * contract.Penalties.AmountOfKmExceedingTheLimit;

        return new RentalReceivingResult(
            actualPeriodInDays,
            delayHours,
            totalConsumption,
            freeKilometers,
            exceededKilometers,
            exceededAmount);
    }
}
