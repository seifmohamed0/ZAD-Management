using System;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.Services.Calculations;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Domain.Entities;

public class RentalContract
{
    public int Id { get; set; }
    public int CompanyId { get; private set; }
    public int BranchId { get; private set; }
    public string ContractNumber { get; private set; } = string.Empty;
    public ContractStatus Status { get; private set; } = ContractStatus.Active;
    public ContractType ContractType { get; private set; } = ContractType.Daily;
    public bool WithDriver { get; private set; }

    public ContractPeriod Period { get; private set; } = null!;
    public TenantSnapshot Tenant { get; private set; } = null!;
    public DriverSnapshot? Driver { get; private set; }
    public RentedVehicleSnapshot Vehicle { get; private set; } = null!;
    public RentalPricing Pricing { get; private set; } = null!;
    public PenaltyPolicy Penalties { get; private set; } = null!;

    public DateTime? ActualReturnDate { get; private set; }
    public decimal? ActualReturnKm { get; private set; }
    public DateTime? ActualReceivingDate { get; private set; }
    public decimal? ReceivingKilometerCounter { get; private set; }
    public decimal? ReceivingPeriodInDays { get; private set; }
    public decimal? ReceivingDelayHours { get; private set; }
    public decimal? ReceivingTotalConsumptionKilometers { get; private set; }
    public decimal? ReceivingFreeKilometers { get; private set; }
    public decimal? ReceivingExceededKilometers { get; private set; }
    public decimal? ReceivingExceededKilometersAmount { get; private set; }
    public bool? MaintenanceDoneByTenant { get; private set; }
    public string? ReceivingNotes { get; private set; }

    public decimal FinalBaseRent { get; private set; }
    public decimal FinalDiscount { get; private set; }
    public decimal FinalDelayPenalty { get; private set; }
    public decimal FinalTotalAmount { get; private set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }

    private RentalContract() { }

    public RentalContract(
        int companyId,
        int branchId,
        string contractNumber,
        ContractType contractType,
        bool withDriver,
        ContractPeriod period,
        TenantSnapshot tenant,
        DriverSnapshot? driver,
        RentedVehicleSnapshot vehicle,
        RentalPricing pricing,
        PenaltyPolicy penalties)
    {
        CompanyId = companyId;
        BranchId = branchId;
        ContractNumber = contractNumber;
        ContractType = contractType;
        WithDriver = driver is not null;
        Status = ContractStatus.Active;

        Period = period ?? throw new ArgumentNullException(nameof(period));
        Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        Driver = driver;
        Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        Pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
        Penalties = penalties ?? throw new ArgumentNullException(nameof(penalties));

        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Close(DateTime actualReturnDate, decimal returnKm, RentalCalculationResult calcResult)
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be closed.");

        if (calcResult == null)
            throw new ArgumentNullException(nameof(calcResult));

        ActualReturnDate = actualReturnDate;
        ActualReturnKm = returnKm;
        FinalBaseRent = calcResult.BaseRent;
        FinalDiscount = calcResult.DiscountAmount;
        FinalDelayPenalty = calcResult.DelayPenalty;
        FinalTotalAmount = calcResult.TotalAmount;
        Status = ContractStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePricing(RentalPricing pricing)
    {
        Pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
    }

    public void UpdateMileagePolicy(decimal kilometerPerDay, decimal maximumKilometerPerDay)
    {
        Vehicle.UpdateMileagePolicy(kilometerPerDay, maximumKilometerPerDay);
    }

    public void UpdatePenaltyPolicy(PenaltyPolicy penalties)
    {
        Penalties = penalties ?? throw new ArgumentNullException(nameof(penalties));
    }

    public void ReceiveVehicle(
        DateTime receivingDate,
        decimal receivingKilometerCounter,
        RentalReceivingResult receivingResult,
        bool maintenanceDoneByTenant,
        string? receivingNotes = null)
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be received.");

        if (receivingKilometerCounter < Vehicle.KilometerCounter)
            throw new ArgumentException(
                "Receiving kilometer counter cannot be less than the starting counter.",
                nameof(receivingKilometerCounter));

        ActualReceivingDate = receivingDate;
        ReceivingKilometerCounter = receivingKilometerCounter;
        ReceivingPeriodInDays = receivingResult.ActualPeriodInDays;
        ReceivingDelayHours = receivingResult.DelayHours;
        ReceivingTotalConsumptionKilometers = receivingResult.TotalConsumptionKilometers;
        ReceivingFreeKilometers = receivingResult.FreeKilometers;
        ReceivingExceededKilometers = receivingResult.ExceededKilometers;
        ReceivingExceededKilometersAmount = receivingResult.ExceededKilometersAmount;
        MaintenanceDoneByTenant = maintenanceDoneByTenant;
        ReceivingNotes = receivingNotes;
        Status = ContractStatus.Received;
        UpdatedAt = DateTime.UtcNow;
    }
}
