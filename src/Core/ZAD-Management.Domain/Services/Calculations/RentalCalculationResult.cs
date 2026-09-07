namespace ZAD_Management.Domain.Services.Calculations;

public record RentalCalculationResult(
    decimal BaseRent,
    decimal DiscountAmount,
    decimal DelayPenalty,
    decimal TotalAmount
);
