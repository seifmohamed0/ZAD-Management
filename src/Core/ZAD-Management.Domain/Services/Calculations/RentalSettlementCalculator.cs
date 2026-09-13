using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public sealed class RentalSettlementCalculator
{
    public RentalCalculationResult Calculate(
        RentalContract contract,
        RentalSettlementInput input)
    {
        ArgumentNullException.ThrowIfNull(contract);
        ArgumentNullException.ThrowIfNull(input);

        if (input.ReturnKilometerCounter < contract.Vehicle.KilometerCounter)
            throw new ArgumentException(
                "Return kilometer counter cannot be less than the starting counter.",
                nameof(input.ReturnKilometerCounter));

        if (input.MaintenancePenaltyAmount < 0 ||
            input.AccidentPenaltyAmount < 0 ||
            input.DriverAmount < 0 ||
            input.PaidAmount < 0 ||
            input.ExitDiscountAmount < 0 ||
            input.MaintenancePaidByTenant < 0)
        {
            throw new ArgumentException("Closing amounts cannot be negative.");
        }

        var calculated = RentalCalculationStrategyFactory.GetStrategy(contract.ContractType)
            .Calculate(contract, input.ActualReturnDate, input.ReturnKilometerCounter);
        var totalConsumption = input.ReturnKilometerCounter - contract.Vehicle.KilometerCounter;
        var freeKilometers = Math.Max(0m, contract.Vehicle.MaximumKilometerPerDay * calculated.ActualPeriodInDays);
        var exceededKilometers = Math.Max(0m, totalConsumption - freeKilometers);
        var exceededAmount = exceededKilometers * contract.Penalties.AmountOfKmExceedingTheLimit;
        var maintenancePenaltyAmount = input.MaintenanceDoneByTenant
            ? 0m
            : input.MaintenancePenaltyAmount;
        var totalAmount = calculated.TotalAmount + maintenancePenaltyAmount +
            input.AccidentPenaltyAmount + input.DriverAmount + exceededAmount -
            input.MaintenancePaidByTenant;
        var actualPeriodInDays = Math.Max(
            1m,
            (decimal)Math.Ceiling((input.ActualReturnDate -
                contract.Period.StartDate.Add(contract.Period.StartTime)).TotalDays));

        return calculated with
        {
            ActualPeriodInDays = actualPeriodInDays,
            TotalConsumptionKilometers = totalConsumption,
            FreeKilometers = freeKilometers,
            ExceededKilometers = exceededKilometers,
            ExceededKilometersAmount = exceededAmount,
            MaintenancePenaltyAmount = maintenancePenaltyAmount,
            AccidentPenaltyAmount = input.AccidentPenaltyAmount,
            DriverAmount = input.DriverAmount,
            PaidAmount = input.PaidAmount,
            ExitDiscountAmount = input.ExitDiscountAmount,
            MaintenancePaidByTenant = input.MaintenancePaidByTenant,
            MaintenanceDoneByTenant = input.MaintenanceDoneByTenant,
            TotalAmount = totalAmount,
            NetDueAmount = Math.Max(0m, totalAmount - input.PaidAmount - input.ExitDiscountAmount),
            DelayHours = RentalPenaltyCalculator.GetBillableDelayHours(
                contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime),
                input.ActualReturnDate,
                contract.Penalties)
        };
    }
}
