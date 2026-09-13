using MediatR;
using ZAD_Management.Domain.Services.Calculations;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CloseRentalContract;

public record CloseRentalContractCommand(
    int ContractId,
    DateTime ActualReturnDate,
    decimal ReturnKm,
    decimal MaintenancePenaltyAmount,
    decimal AccidentPenaltyAmount,
    decimal DriverAmount,
    decimal PaidAmount,
    string? Notes,
    decimal ExitDiscountAmount = 0,
    decimal MaintenancePaidByTenant = 0,
    bool MaintenanceDoneByTenant = false
) : IRequest<RentalCalculationResult>;

