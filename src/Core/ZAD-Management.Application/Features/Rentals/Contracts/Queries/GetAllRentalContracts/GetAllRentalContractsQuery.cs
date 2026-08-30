using MediatR;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetAllRentalContracts;

public record GetAllRentalContractsQuery(int? BranchId = null) : IRequest<List<RentalContractListDto>>;

