using ZAD_Management.Domain.Enums;

namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class CreateRentalContractDto
{
    // Settings
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public string? ReferenceNo { get; set; }
    public string? AccountingNo { get; set; }
    public string Currency { get; set; } = "SAR";
    public ContractType ContractType { get; set; } = ContractType.Daily;
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;
    public bool WithDriver { get; set; }
    public string? DriverName { get; set; }
    public string? Notes { get; set; }

    // Period
    public DateTime StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateTime ExpectedReceivingDate { get; set; }
    public TimeSpan ExpectedReceivingTime { get; set; }
    public int? PeriodInDays { get; set; }

    // Tab 1: Tenant
    public TenantDto Tenant { get; set; } = new();
    public SponsorDto? Sponsor { get; set; }
    public SecondDriverDto? SecondDriver { get; set; }

    // Tab 2: Vehicle Info
    public VehicleInfoDto Vehicle { get; set; } = new();
    public PricingDto Pricing { get; set; } = new();
    public PenaltiesDto Penalties { get; set; } = new();
    public PrivateDriverTermsDto? DriverTerms { get; set; }
    public MileagePolicyDto Mileage { get; set; } = new();
    public MaintenanceAlertDto? Maintenance { get; set; }
}

public class TenantDto
{
    public string TenantName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string? PassportNumber { get; set; }
    public string? UnifiedNumber { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public DateTime? TenantBirthday { get; set; }
}

public class SponsorDto
{
    public string? SponsorName { get; set; }
    public string? Nationality { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime? LicenseExpireDate { get; set; }
    public string? IdNumber { get; set; }
    public DateTime? IdExpireDate { get; set; }
}

public class SecondDriverDto
{
    public string? SecondDriverName { get; set; }
    public string? Nationality { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime? LicenseExpireDate { get; set; }
    public string? IdNumber { get; set; }
    public DateTime? IdExpireDate { get; set; }
}

public class VehicleInfoDto
{
    public string PlateNo { get; set; } = string.Empty;
    public string ModelYear { get; set; } = string.Empty;
    public string FileNo { get; set; } = string.Empty;
    public decimal StartKilometerCounter { get; set; }
}

public class PricingDto
{
    public decimal RentPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
}

public class PenaltiesDto
{
    public decimal DelayPenaltyPerHour { get; set; }
    public decimal AllowedDelayHours { get; set; }
    public decimal MaintenancePenalty { get; set; }
    public decimal AccidentPenalty { get; set; }
}

public class PrivateDriverTermsDto
{
    public decimal DriverFare { get; set; }
    public decimal DriverWorkingHoursPerDay { get; set; }
    public decimal DriverOvertimeAmountPerHour { get; set; }
    public decimal DailyRate { get; set; }
}

public class MileagePolicyDto
{
    public decimal KilometerPerDay { get; set; }
    public decimal MaximumKilometerPerDay { get; set; }
    public decimal AmountOfKmExceedingLimit { get; set; }
}

public class MaintenanceAlertDto
{
    public DateTime? NextMaintenanceDate { get; set; }
    public decimal? NextMaintenanceKm { get; set; }
    public int ReminderBeforePeriodicMaintenance { get; set; } = 7;
    public NotificationType NotificationType { get; set; } = NotificationType.Kilometer;
}

