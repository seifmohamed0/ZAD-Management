namespace ZAD_Management.Domain.Services.Calculations;

public sealed record RentalSettlementInput(
    DateTime ActualReturnDate,
    decimal ReturnKilometerCounter,
    decimal MaintenancePenaltyAmount,
    decimal AccidentPenaltyAmount,
    decimal DriverAmount,
    decimal PaidAmount,
    decimal ExitDiscountAmount,
    decimal MaintenancePaidByTenant,
    bool MaintenanceDoneByTenant);
