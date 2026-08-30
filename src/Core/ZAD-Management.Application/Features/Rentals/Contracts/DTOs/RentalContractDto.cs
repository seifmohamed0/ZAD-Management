using ZAD_Management.Domain.Enums;

namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class RentalContractDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int BranchId { get; set; }
    public string? BranchName { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string? AccountingNo { get; set; }
    public string? ReferenceNo { get; set; }
    public string Currency { get; set; } = "SAR";
    public ContractStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public ContractType ContractType { get; set; }
    public string ContractTypeName => ContractType.ToString();
    public PaymentType PaymentType { get; set; }
    public string PaymentTypeName => PaymentType.ToString();
    public bool WithDriver { get; set; }
    public string? DriverName { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Period
    public DateTime StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public string StartDay { get; set; } = string.Empty;
    public DateTime ExpectedReceivingDate { get; set; }
    public TimeSpan ExpectedReceivingTime { get; set; }
    public string DeliveryDay { get; set; } = string.Empty;
    public int PeriodInDays { get; set; }
    public int ActualPeriodInDays { get; set; }

    // Tenant
    public string TenantName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string? PassportNumber { get; set; }
    public string? UnifiedNumber { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public DateTime? TenantBirthday { get; set; }
    public int? TenantAge { get; set; }

    // Sponsor
    public string? SponsorName { get; set; }
    public string? SponsorNationality { get; set; }
    public string? SponsorLicenseNumber { get; set; }
    public DateTime? SponsorLicenseExpireDate { get; set; }
    public string? SponsorIdNumber { get; set; }
    public DateTime? SponsorIdExpireDate { get; set; }

    // Second Driver
    public string? SecondDriverName { get; set; }
    public string? SecondDriverNationality { get; set; }
    public string? SecondDriverLicenseNumber { get; set; }
    public DateTime? SecondDriverLicenseExpireDate { get; set; }
    public string? SecondDriverIdNumber { get; set; }
    public DateTime? SecondDriverIdExpireDate { get; set; }

    // Vehicle
    public string VehiclePlateNo { get; set; } = string.Empty;
    public string VehicleModelYear { get; set; } = string.Empty;
    public string VehicleFileNo { get; set; } = string.Empty;
    public decimal StartKilometerCounter { get; set; }
    public decimal? ReturnKilometerCounter { get; set; }

    // Pricing
    public decimal RentPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal NetRentPrice { get; set; }

    // Penalties
    public decimal DelayPenaltyPerHour { get; set; }
    public decimal AllowedDelayHours { get; set; }
    public decimal MaintenancePenalty { get; set; }
    public decimal AccidentPenalty { get; set; }

    // Driver Terms
    public decimal? DriverFare { get; set; }
    public decimal? DriverWorkingHoursPerDay { get; set; }
    public decimal? DriverOvertimeAmountPerHour { get; set; }
    public decimal? DriverDailyRate { get; set; }

    // Mileage Policy
    public decimal KilometerPerDay { get; set; }
    public decimal MaximumKilometerPerDay { get; set; }
    public decimal AmountOfKmExceedingLimit { get; set; }

    // Maintenance
    public DateTime? NextMaintenanceDate { get; set; }
    public decimal? NextMaintenanceKm { get; set; }
    public int? ReminderBeforePeriodicMaintenance { get; set; }
    public NotificationType? NotificationType { get; set; }
}

