using ZAD_Management.Domain.Common;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Domain.Entities;

public class RentalContract : BaseEntity
{
    public int CompanyId { get; private set; }
    public int BranchId { get; private set; }
    public string ContractNumber { get; private set; } = string.Empty;
    public string? AccountingNo { get; private set; }
    public string? ReferenceNo { get; private set; }
    public string Currency { get; private set; } = "EGP";
    public ContractStatus Status { get; private set; } = ContractStatus.Draft;
    public ContractType ContractType { get; private set; } = ContractType.Daily;
    public PaymentType PaymentType { get; private set; } = PaymentType.Cash;
    public bool WithDriver { get; private set; }
    public string? DriverName { get; private set; }
    public string? Notes { get; private set; }

    public ContractPeriod Period { get; private set; } = null!;
    public TenantSnapshot Tenant { get; private set; } = null!;
    public SponsorSnapshot? Sponsor { get; private set; }
    public DriverSnapshot? SecondDriver { get; private set; }
    public RentedVehicleSnapshot Vehicle { get; private set; } = null!;
    public RentalPricing Pricing { get; private set; } = null!;
    public PenaltyPolicy Penalties { get; private set; } = null!;
    public PrivateDriverTerms? DriverTerms { get; private set; }
    public MileagePolicy Mileage { get; private set; } = null!;
    public MaintenanceAlert? Maintenance { get; private set; }

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }

    private RentalContract() { }

    public RentalContract(
        int companyId,
        int branchId,
        string contractNumber,
        string? accountingNo,
        string? referenceNo,
        string currency,
        ContractType contractType,
        PaymentType paymentType,
        bool withDriver,
        string? driverName,
        string? notes,
        ContractPeriod period,
        TenantSnapshot tenant,
        SponsorSnapshot? sponsor,
        DriverSnapshot? secondDriver,
        RentedVehicleSnapshot vehicle,
        RentalPricing pricing,
        PenaltyPolicy penalties,
        PrivateDriverTerms? driverTerms,
        MileagePolicy mileage,
        MaintenanceAlert? maintenance)
    {
        CompanyId = companyId;
        BranchId = branchId;
        ContractNumber = contractNumber;
        AccountingNo = accountingNo;
        ReferenceNo = referenceNo;
        Currency = string.IsNullOrWhiteSpace(currency) ? "EGP" : currency;
        Status = ContractStatus.Draft;
        ContractType = contractType;
        PaymentType = paymentType;
        WithDriver = withDriver;
        DriverName = driverName;
        Notes = notes;

        Period = period ?? throw new ArgumentNullException(nameof(period));
        Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        Sponsor = sponsor;
        SecondDriver = secondDriver;
        Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        Pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
        Penalties = penalties ?? throw new ArgumentNullException(nameof(penalties));
        DriverTerms = driverTerms;
        Mileage = mileage ?? throw new ArgumentNullException(nameof(mileage));
        Maintenance = maintenance;
    }

    public void Activate()
    {
        if (Status != ContractStatus.Draft)
            throw new InvalidOperationException("Only draft contracts can be activated.");

        Status = ContractStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close(DateTime actualReturnDate, decimal returnKm)
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be closed.");

        Period.RecordActualReturn(actualReturnDate);
        Vehicle.RecordReturnKilometer(returnKm);
        Status = ContractStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == ContractStatus.Closed)
            throw new InvalidOperationException("Closed contracts cannot be cancelled.");

        Status = ContractStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}

