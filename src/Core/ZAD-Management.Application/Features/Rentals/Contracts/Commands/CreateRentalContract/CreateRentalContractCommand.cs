using MediatR;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CreateRentalContract;

public record CreateRentalContractCommand(
    CreateRentalContractDto Contract
) : IRequest<int>;

