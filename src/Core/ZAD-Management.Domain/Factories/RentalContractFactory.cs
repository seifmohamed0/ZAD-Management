using System;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Domain.Factories;

public class RentalContractFactory : IRentalContractFactory
{
    public RentalContract Create(
        int companyId,
        int branchId,
        ContractType contractType,
        bool withDriver,
        ContractPeriod period,
        TenantSnapshot tenant,
        DriverSnapshot? driver,
        RentedVehicleSnapshot vehicle,
        RentalPricing pricing,
        PenaltyPolicy penalties)
    {
        if (companyId <= 0)
            throw new ArgumentException("Company ID must be greater than zero.", nameof(companyId));

        if (branchId <= 0)
            throw new ArgumentException("Branch ID must be greater than zero.", nameof(branchId));

        if (period == null)
            throw new ArgumentNullException(nameof(period));

        if (tenant == null)
            throw new ArgumentNullException(nameof(tenant));

        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle));

        if (pricing == null)
            throw new ArgumentNullException(nameof(pricing));

        if (penalties == null)
            throw new ArgumentNullException(nameof(penalties));

        var contractNumber = $"RC-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

        return new RentalContract(
            companyId,
            branchId,
            contractNumber,
            contractType,
            withDriver,
            period,
            tenant,
            driver,
            vehicle,
            pricing,
            penalties);
    }
}

