using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Domain.Factories;

public interface IRentalContractFactory
{
    RentalContract Create(
        int companyId,
        int branchId,
        ContractType contractType,
        bool withDriver,
        ContractPeriod period,
        TenantSnapshot tenant,
        DriverSnapshot? driver,
        RentedVehicleSnapshot vehicle,
        RentalPricing pricing,
        PenaltyPolicy penalties);
}

