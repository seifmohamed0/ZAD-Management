namespace ZAD_Management.Domain.Services.Calculations;

public record RentalReceivingResult(
    decimal ActualPeriodInDays,
    decimal DelayHours,
    decimal TotalConsumptionKilometers,
    decimal FreeKilometers,
    decimal ExceededKilometers,
    decimal ExceededKilometersAmount);