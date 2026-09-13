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
            CompanyName = null,
            BranchId = contract.BranchId,
            BranchName = null,
            ContractNumber = contract.ContractNumber,
            AccountingNo = null,
            ReferenceNo = null,
            Currency = "SAR",
            Status = contract.Status,
            ContractType = contract.ContractType,
            PaymentType = Domain.Enums.PaymentType.Cash,
            WithDriver = contract.WithDriver,
            DriverName = contract.Driver?.DriverName,
            Notes = null,
            CreatedAt = contract.CreatedAt,

            // Period
            StartDate = contract.Period.StartDate,
            StartTime = contract.Period.StartTime,
            StartDay = contract.Period.StartDate.DayOfWeek.ToString(),
            ExpectedReceivingDate = contract.Period.ExpectedReceivingDate,
            ExpectedReceivingTime = contract.Period.ExpectedReceivingTime,
            DeliveryDay = contract.Period.ExpectedReceivingDate.DayOfWeek.ToString(),
            PeriodInDays = contract.Period.PeriodInDays,
            ActualPeriodInDays = contract.Period.PeriodInDays,

            // Tenant
            TenantName = contract.Tenant.TenantName,
            LicenseNumber = contract.Tenant.LicenseNumber,
            PassportNumber = null,
            UnifiedNumber = null,
            IdNumber = contract.Tenant.IdNumber,
            Mobile = contract.Tenant.Mobile,
            TenantBirthday = null,
            TenantAge = 0,

            // Sponsor
            SponsorName = null,
            SponsorNationality = null,
            SponsorLicenseNumber = null,
            SponsorLicenseExpireDate = null,
            SponsorIdNumber = null,
            SponsorIdExpireDate = null,

            // Driver
            SecondDriverName = contract.Driver?.DriverName,
            SecondDriverNationality = contract.Driver?.Nationality,
            SecondDriverLicenseNumber = contract.Driver?.LicenseNumber,
            SecondDriverLicenseExpireDate = contract.Driver?.LicenseExpireDate,
            SecondDriverIdNumber = contract.Driver?.IdNumber,
            SecondDriverIdExpireDate = contract.Driver?.IdExpireDate,

            // Vehicle
            VehiclePlateNo = contract.Vehicle.PlateNo,
            VehicleModelYear = contract.Vehicle.ModelYear,
            VehicleFileNo = contract.Vehicle.FileNo,
            StartKilometerCounter = contract.Vehicle.KilometerCounter,
            ReturnKilometerCounter = null,

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
            DriverFare = null,
            DriverWorkingHoursPerDay = null,
            DriverOvertimeAmountPerHour = null,
            DriverDailyRate = null,

            // Mileage
            KilometerPerDay = contract.Vehicle.KilometerPerDay,
            MaximumKilometerPerDay = contract.Vehicle.MaximumKilometerPerDay,
                AmountOfKmExceedingLimit = contract.Penalties.AmountOfKmExceedingTheLimit,

            // Maintenance
            NextMaintenanceDate = contract.Vehicle.NextMaintenanceDate,
            NextMaintenanceKm = contract.Vehicle.NextMaintenanceKm,
            ReminderBeforePeriodicMaintenance = null,
            NotificationType = null
        };
    }
}
