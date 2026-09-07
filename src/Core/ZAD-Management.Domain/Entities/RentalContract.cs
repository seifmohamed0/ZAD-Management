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

    // Return & Closing fields
    public DateTime? ActualReturnDate { get; private set; }
    public decimal? ActualReturnKm { get; private set; }

    // Final Invoice
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
        WithDriver = withDriver;
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
}
