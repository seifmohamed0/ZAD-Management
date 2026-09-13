namespace ZAD_Management.Domain.Services.Calculations;

public record RentalCalculationResult(
    decimal BaseRent,
    decimal DiscountAmount,
    decimal DelayPenalty,
    decimal TotalAmount,
    decimal ActualPeriodInDays = 0,
    decimal DelayHours = 0,
    decimal TotalConsumptionKilometers = 0,
    decimal FreeKilometers = 0,
    decimal ExceededKilometers = 0,
    decimal ExceededKilometersAmount = 0,
    decimal MaintenancePenaltyAmount = 0,
    decimal AccidentPenaltyAmount = 0,
    decimal DriverAmount = 0,
    decimal PaidAmount = 0,
    decimal NetDueAmount = 0,
    decimal ExitDiscountAmount = 0,
    decimal MaintenancePaidByTenant = 0,
    bool MaintenanceDoneByTenant = false
);
