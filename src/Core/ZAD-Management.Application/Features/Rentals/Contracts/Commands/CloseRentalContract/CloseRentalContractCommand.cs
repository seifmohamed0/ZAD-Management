using MediatR;
using ZAD_Management.Domain.Services.Calculations;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CloseRentalContract;

public record CloseRentalContractCommand(
    int ContractId,
    DateTime ActualReturnDate,
    decimal ReturnKm
) : IRequest<RentalCalculationResult>;

