using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.Factories;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CreateRentalContract;

public class CreateRentalContractHandler
    : IRequestHandler<CreateRentalContractCommand, int>
{
    private readonly IRentalContractRepository _contractRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IRentalContractFactory _contractFactory;

    public CreateRentalContractHandler(
        IRentalContractRepository contractRepository,
        ICompanyRepository companyRepository,
        IBranchRepository branchRepository,
        IRentalContractFactory contractFactory)
    {
        _contractRepository = contractRepository;
        _companyRepository = companyRepository;
        _branchRepository = branchRepository;
        _contractFactory = contractFactory;
    }

    public async Task<int> Handle(
        CreateRentalContractCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Contract;

        // Validate Company
        var company = await _companyRepository.GetByIdAsync(
            dto.CompanyId,
            cancellationToken);

        if (company == null)
            throw new ArgumentException(
                $"Company with ID {dto.CompanyId} was not found.");

        // Validate Branch
        var branch = await _branchRepository.GetByIdAsync(
            dto.BranchId,
            cancellationToken);

        if (branch == null)
            throw new ArgumentException(
                $"Branch with ID {dto.BranchId} was not found.");

        // Create Contract Period
        var period = new ContractPeriod(
            dto.StartDate,
            dto.StartTime,
            dto.ExpectedReceivingDate,
            dto.ExpectedReceivingTime,
            dto.PeriodInDays
        );

        // Create Tenant Snapshot
        var tenant = new TenantSnapshot(
            dto.Tenant.TenantName,
            dto.Tenant.LicenseNumber,
            dto.Tenant.IdNumber,
            dto.Tenant.Mobile
        );

        // Create Driver Snapshot
        DriverSnapshot? driver = null;

        if (dto.SecondDriver != null &&
            !string.IsNullOrWhiteSpace(dto.SecondDriver.SecondDriverName))
        {
            driver = new DriverSnapshot(
                dto.SecondDriver.SecondDriverName,
                dto.SecondDriver.Nationality,
                dto.SecondDriver.LicenseNumber,
                dto.SecondDriver.LicenseExpireDate,
                dto.SecondDriver.IdNumber,
                dto.SecondDriver.IdExpireDate
            );
        }

        // Create Vehicle Snapshot
        var vehicle = new RentedVehicleSnapshot(
            dto.Vehicle.PlateNo,
            dto.Vehicle.ModelYear,
            dto.Vehicle.StartKilometerCounter,
            dto.Mileage.KilometerPerDay,
            dto.Mileage.MaximumKilometerPerDay,
            dto.Maintenance?.NextMaintenanceDate,
            dto.Maintenance?.NextMaintenanceKm,
            dto.Vehicle.FileNo
        );

        // Create Rental Pricing
        var pricing = new RentalPricing(
            dto.Pricing.RentPrice,
            dto.Pricing.DiscountPercent,
            dto.Pricing.DiscountAmount
        );

        // Create Penalty Policy
        var penalties = new PenaltyPolicy(
            dto.Penalties.DelayPenaltyPerHour,
            dto.Penalties.AllowedDelayHours,
            dto.Penalties.MaintenancePenalty,
            dto.Penalties.AccidentPenalty,
            dto.Mileage.AmountOfKmExceedingLimit
        );

        // Create Rental Contract via Domain Factory
        var contract = _contractFactory.Create(
            dto.CompanyId,
            dto.BranchId,
            dto.ContractType,
            dto.WithDriver,
            period,
            tenant,
            driver,
            vehicle,
            pricing,
            penalties
        );

        // Save Contract
        return await _contractRepository.AddAsync(
            contract,
            cancellationToken);
    }
}
