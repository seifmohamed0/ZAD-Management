using MediatR;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetRentalContractById;

public record GetRentalContractByIdQuery(int Id) : IRequest<RentalContractDto?>;

