using MediatR;
using ZAD_Management.Domain.Services.Calculations;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.ReceiveVehicle;

public record ReceiveVehicleCommand(
    int ContractId,
    DateTime ReceivingDate,
    decimal ReceivingKilometerCounter,
    bool MaintenanceDoneByTenant,
    string? Notes
) : IRequest<RentalReceivingResult>;
