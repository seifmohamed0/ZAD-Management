using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CreateRentalContract;

public class CreateRentalContractHandler : IRequestHandler<CreateRentalContractCommand, int>
{
    private readonly IRentalContractRepository _contractRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IBranchRepository _branchRepository;

    public CreateRentalContractHandler(
        IRentalContractRepository contractRepository,
        ICompanyRepository companyRepository,
        IBranchRepository branchRepository)
    {
        _contractRepository = contractRepository;
        _companyRepository = companyRepository;
        _branchRepository = branchRepository;
    }

    public async Task<int> Handle(CreateRentalContractCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Contract;

        var company = await _companyRepository.GetByIdAsync(dto.CompanyId, cancellationToken);
        if (company == null)
            throw new ArgumentException($"Company with ID {dto.CompanyId} was not found.");

        var branch = await _branchRepository.GetByIdAsync(dto.BranchId, cancellationToken);
        if (branch == null)
            throw new ArgumentException($"Branch with ID {dto.BranchId} was not found.");

        string contractNumber = $"RC-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

        var period = new ContractPeriod(
            dto.StartDate,
            dto.StartTime,
            dto.ExpectedReceivingDate,
            dto.ExpectedReceivingTime,
            dto.PeriodInDays
        );

        var tenant = new TenantSnapshot(
            dto.Tenant.TenantName,
            dto.Tenant.LicenseNumber,
            dto.Tenant.IdNumber,
            dto.Tenant.Mobile,
            dto.Tenant.PassportNumber,
            dto.Tenant.UnifiedNumber,
            dto.Tenant.TenantBirthday
        );

        SponsorSnapshot? sponsor = null;
        if (dto.Sponsor != null && !string.IsNullOrWhiteSpace(dto.Sponsor.SponsorName))
        {
            sponsor = new SponsorSnapshot(
                dto.Sponsor.SponsorName,
                dto.Sponsor.Nationality,
                dto.Sponsor.LicenseNumber,
                dto.Sponsor.LicenseExpireDate,
                dto.Sponsor.IdNumber,
                dto.Sponsor.IdExpireDate
            );
        }

        DriverSnapshot? secondDriver = null;
        if (dto.SecondDriver != null && !string.IsNullOrWhiteSpace(dto.SecondDriver.SecondDriverName))
        {
            secondDriver = new DriverSnapshot(
                dto.SecondDriver.SecondDriverName,
                dto.SecondDriver.Nationality,
                dto.SecondDriver.LicenseNumber,
                dto.SecondDriver.LicenseExpireDate,
                dto.SecondDriver.IdNumber,
                dto.SecondDriver.IdExpireDate
            );
        }

        var vehicle = new RentedVehicleSnapshot(
            dto.Vehicle.PlateNo,
            dto.Vehicle.ModelYear,
            dto.Vehicle.FileNo,
            dto.Vehicle.StartKilometerCounter
        );

        var pricing = new RentalPricing(
            dto.Pricing.RentPrice,
            dto.Pricing.DiscountPercent,
            dto.Pricing.DiscountAmount
        );

        var penalties = new PenaltyPolicy(
            dto.Penalties.DelayPenaltyPerHour,
            dto.Penalties.AllowedDelayHours,
            dto.Penalties.MaintenancePenalty,
            dto.Penalties.AccidentPenalty
        );

        PrivateDriverTerms? driverTerms = null;
        if (dto.WithDriver || (dto.DriverTerms != null && dto.DriverTerms.DailyRate > 0))
        {
            driverTerms = new PrivateDriverTerms(
                dto.DriverTerms?.DriverFare ?? 0,
                dto.DriverTerms?.DriverWorkingHoursPerDay ?? 8,
                dto.DriverTerms?.DriverOvertimeAmountPerHour ?? 0,
                dto.DriverTerms?.DailyRate ?? 0
            );
        }

        var mileage = new MileagePolicy(
            dto.Mileage.KilometerPerDay,
            dto.Mileage.MaximumKilometerPerDay,
            dto.Mileage.AmountOfKmExceedingLimit
        );

        MaintenanceAlert? maintenance = null;
        if (dto.Maintenance != null && (dto.Maintenance.NextMaintenanceDate.HasValue || dto.Maintenance.NextMaintenanceKm.HasValue))
        {
            maintenance = new MaintenanceAlert(
                dto.Maintenance.NextMaintenanceDate,
                dto.Maintenance.NextMaintenanceKm,
                dto.Maintenance.ReminderBeforePeriodicMaintenance,
                dto.Maintenance.NotificationType
            );
        }


        // ال aggregate root وصل يا حارة
        var contract = new RentalContract(
            dto.CompanyId,
            dto.BranchId,
            contractNumber,
            dto.AccountingNo,
            dto.ReferenceNo,
            dto.Currency,
            dto.ContractType,
            dto.PaymentType,
            dto.WithDriver,
            dto.DriverName,
            dto.Notes,
            period,
            tenant,
            sponsor,
            secondDriver,
            vehicle,
            pricing,
            penalties,
            driverTerms,
            mileage,
            maintenance
        );

        return await _contractRepository.AddAsync(contract, cancellationToken);
    }
}

