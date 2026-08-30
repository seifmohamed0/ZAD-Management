using MediatR;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetAllRentalContracts;

public class GetAllRentalContractsHandler : IRequestHandler<GetAllRentalContractsQuery, List<RentalContractListDto>>
{
    private readonly IRentalContractRepository _contractRepository;

    public GetAllRentalContractsHandler(IRentalContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<List<RentalContractListDto>> Handle(GetAllRentalContractsQuery request, CancellationToken cancellationToken)
    {
        var contracts = request.BranchId.HasValue
            ? await _contractRepository.GetByBranchIdAsync(request.BranchId.Value, cancellationToken)
            : await _contractRepository.GetAllAsync(cancellationToken);

        return contracts.Select(c => new RentalContractListDto
        {
            Id = c.Id,
            ContractNumber = c.ContractNumber,
            ReferenceNo = c.ReferenceNo,
            CompanyName = c.Company?.EnglishName ?? string.Empty,
            BranchName = c.Branch?.EnglishName ?? string.Empty,
            TenantName = c.Tenant.TenantName,
            TenantMobile = c.Tenant.Mobile,
            VehiclePlateNo = c.Vehicle.PlateNo,
            StartDate = c.Period.StartDate,
            ExpectedReceivingDate = c.Period.ExpectedReceivingDate,
            PeriodInDays = c.Period.PeriodInDays,
            NetRentPrice = c.Pricing.NetRentPrice,
            Currency = c.Currency,
            Status = c.Status,
            ContractType = c.ContractType
        }).ToList();
    }
}

