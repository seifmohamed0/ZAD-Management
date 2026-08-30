using MediatR;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetRentalContractById;

public class GetRentalContractByIdHandler : IRequestHandler<GetRentalContractByIdQuery, RentalContractDto?>
{
    private readonly IRentalContractRepository _contractRepository;

    public GetRentalContractByIdHandler(IRentalContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<RentalContractDto?> Handle(GetRentalContractByIdQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.Id, cancellationToken);
        if (contract == null)
            return null;

        return new RentalContractDto
        {
            Id = contract.Id,
            CompanyId = contract.CompanyId,
            CompanyName = contract.Company?.EnglishName,
            BranchId = contract.BranchId,
            BranchName = contract.Branch?.EnglishName,
            ContractNumber = contract.ContractNumber,
            AccountingNo = contract.AccountingNo,
            ReferenceNo = contract.ReferenceNo,
            Currency = contract.Currency,
            Status = contract.Status,
            ContractType = contract.ContractType,
            PaymentType = contract.PaymentType,
            WithDriver = contract.WithDriver,
            DriverName = contract.DriverName,
            Notes = contract.Notes,
            CreatedAt = contract.CreatedAt,

            // Period
            StartDate = contract.Period.StartDate,
            StartTime = contract.Period.StartTime,
            StartDay = contract.Period.StartDay,
            ExpectedReceivingDate = contract.Period.ExpectedReceivingDate,
            ExpectedReceivingTime = contract.Period.ExpectedReceivingTime,
            DeliveryDay = contract.Period.DeliveryDay,
            PeriodInDays = contract.Period.PeriodInDays,
            ActualPeriodInDays = contract.Period.ActualPeriodInDays,

            // Tenant
            TenantName = contract.Tenant.TenantName,
            LicenseNumber = contract.Tenant.LicenseNumber,
            PassportNumber = contract.Tenant.PassportNumber,
            UnifiedNumber = contract.Tenant.UnifiedNumber,
            IdNumber = contract.Tenant.IdNumber,
            Mobile = contract.Tenant.Mobile,
            TenantBirthday = contract.Tenant.TenantBirthday,
            TenantAge = contract.Tenant.Age,

            // Sponsor
            SponsorName = contract.Sponsor?.SponsorName,
            SponsorNationality = contract.Sponsor?.Nationality,
            SponsorLicenseNumber = contract.Sponsor?.LicenseNumber,
            SponsorLicenseExpireDate = contract.Sponsor?.LicenseExpireDate,
            SponsorIdNumber = contract.Sponsor?.IdNumber,
            SponsorIdExpireDate = contract.Sponsor?.IdExpireDate,

            // Second Driver
            SecondDriverName = contract.SecondDriver?.SecondDriverName,
            SecondDriverNationality = contract.SecondDriver?.Nationality,
            SecondDriverLicenseNumber = contract.SecondDriver?.LicenseNumber,
            SecondDriverLicenseExpireDate = contract.SecondDriver?.LicenseExpireDate,
            SecondDriverIdNumber = contract.SecondDriver?.IdNumber,
            SecondDriverIdExpireDate = contract.SecondDriver?.IdExpireDate,

            // Vehicle
            VehiclePlateNo = contract.Vehicle.PlateNo,
            VehicleModelYear = contract.Vehicle.ModelYear,
            VehicleFileNo = contract.Vehicle.FileNo,
            StartKilometerCounter = contract.Vehicle.StartKilometerCounter,
            ReturnKilometerCounter = contract.Vehicle.ReturnKilometerCounter,

            // Pricing
            RentPrice = contract.Pricing.RentPrice,
            DiscountPercent = contract.Pricing.DiscountPercent,
            DiscountAmount = contract.Pricing.DiscountAmount,
            NetRentPrice = contract.Pricing.NetRentPrice,

            // Penalties
            DelayPenaltyPerHour = contract.Penalties.DelayPenaltyPerHour,
            AllowedDelayHours = contract.Penalties.AllowedDelayHours,
            MaintenancePenalty = contract.Penalties.MaintenancePenalty,
            AccidentPenalty = contract.Penalties.AccidentPenalty,

            // Driver Terms
            DriverFare = contract.DriverTerms?.DriverFare,
            DriverWorkingHoursPerDay = contract.DriverTerms?.DriverWorkingHoursPerDay,
            DriverOvertimeAmountPerHour = contract.DriverTerms?.DriverOvertimeAmountPerHour,
            DriverDailyRate = contract.DriverTerms?.DailyRate,

            // Mileage
            KilometerPerDay = contract.Mileage.KilometerPerDay,
            MaximumKilometerPerDay = contract.Mileage.MaximumKilometerPerDay,
            AmountOfKmExceedingLimit = contract.Mileage.AmountOfKmExceedingLimit,

            // Maintenance
            NextMaintenanceDate = contract.Maintenance?.NextMaintenanceDate,
            NextMaintenanceKm = contract.Maintenance?.NextMaintenanceKm,
            ReminderBeforePeriodicMaintenance = contract.Maintenance?.ReminderBeforePeriodicMaintenance,
            NotificationType = contract.Maintenance?.NotificationType
        };
    }
}

