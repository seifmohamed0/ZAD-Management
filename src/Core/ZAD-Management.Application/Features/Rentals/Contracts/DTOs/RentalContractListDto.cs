using ZAD_Management.Domain.Enums;

namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class RentalContractListDto
{
    public int Id { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string? ReferenceNo { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string TenantMobile { get; set; } = string.Empty;
    public string VehiclePlateNo { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime ExpectedReceivingDate { get; set; }
    public int PeriodInDays { get; set; }
    public decimal NetRentPrice { get; set; }
    public string Currency { get; set; } = "SAR";
    public ContractStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public ContractType ContractType { get; set; }
    public string ContractTypeName => ContractType.ToString();
}

