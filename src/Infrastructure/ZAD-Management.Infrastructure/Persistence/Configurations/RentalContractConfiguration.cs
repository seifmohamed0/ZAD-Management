using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainRentalContract = ZAD_Management.Domain.Entities.RentalContract;

namespace ZAD_Management.Infrastructure.Persistence.Configurations;

public class RentalContractConfiguration : IEntityTypeConfiguration<DomainRentalContract>
{
    public void Configure(EntityTypeBuilder<DomainRentalContract> builder)
    {
        builder.ToTable("RentalContracts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContractNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.BranchId).IsRequired();

        builder.OwnsOne(x => x.Period, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            period.Property(p => p.StartTime).HasColumnName("StartTime").IsRequired();
            period.Property(p => p.ExpectedReceivingDate).HasColumnName("ExpectedReceivingDate").IsRequired();
            period.Property(p => p.ExpectedReceivingTime).HasColumnName("ExpectedReceivingTime").IsRequired();
            period.Property(p => p.PeriodInDays).HasColumnName("PeriodInDays").IsRequired();
        });

        builder.OwnsOne(x => x.Tenant, tenant =>
        {
            tenant.Property(t => t.TenantName).HasColumnName("TenantName").HasMaxLength(200).IsRequired();
            tenant.Property(t => t.LicenseNumber).HasColumnName("TenantLicenseNumber").HasMaxLength(50).IsRequired();
            tenant.Property(t => t.IdNumber).HasColumnName("TenantIdNumber").HasMaxLength(50).IsRequired();
            tenant.Property(t => t.Mobile).HasColumnName("TenantMobile").HasMaxLength(25).IsRequired();
        });

        builder.OwnsOne(x => x.Driver, driver =>
        {
            driver.Property(d => d.DriverName).HasColumnName("DriverName").HasMaxLength(200);
            driver.Property(d => d.Nationality).HasColumnName("DriverNationality").HasMaxLength(100);
            driver.Property(d => d.LicenseNumber).HasColumnName("DriverLicenseNumber").HasMaxLength(50);
            driver.Property(d => d.LicenseExpireDate).HasColumnName("DriverLicenseExpireDate");
            driver.Property(d => d.IdNumber).HasColumnName("DriverIdNumber").HasMaxLength(50);
            driver.Property(d => d.IdExpireDate).HasColumnName("DriverIdExpireDate");
        });

        builder.OwnsOne(x => x.Vehicle, vehicle =>
        {
            vehicle.Property(v => v.PlateNo).HasColumnName("VehiclePlateNo").HasMaxLength(50).IsRequired();
            vehicle.Property(v => v.ModelYear).HasColumnName("VehicleModelYear").HasMaxLength(10);
            vehicle.Property(v => v.FileNo).HasColumnName("VehicleFileNo").HasMaxLength(50);
            vehicle.Property(v => v.KilometerCounter).HasColumnName("StartKilometerCounter").HasPrecision(18, 2);
            vehicle.Property(v => v.KilometerPerDay).HasColumnName("KilometerPerDay").HasPrecision(18, 2);
            vehicle.Property(v => v.MaximumKilometerPerDay).HasColumnName("MaximumKilometerPerDay").HasPrecision(18, 2);
            vehicle.Property(v => v.NextMaintenanceDate).HasColumnName("NextMaintenanceDate");
            vehicle.Property(v => v.NextMaintenanceKm).HasColumnName("NextMaintenanceKm").HasPrecision(18, 2);
        });

        builder.OwnsOne(x => x.Pricing, pricing =>
        {
            pricing.Property(p => p.RentPrice).HasColumnName("RentPrice").HasPrecision(18, 2).IsRequired();
            pricing.Property(p => p.DiscountPercent).HasColumnName("DiscountPercent").HasPrecision(5, 2);
            pricing.Property(p => p.DiscountAmount).HasColumnName("DiscountAmount").HasPrecision(18, 2);
            pricing.Property(p => p.NetRentPrice).HasColumnName("NetRentPrice").HasPrecision(18, 2).IsRequired();
        });

        builder.OwnsOne(x => x.Penalties, penalties =>
        {
            penalties.Property(p => p.DelayPenaltyPerHour).HasColumnName("DelayPenaltyPerHour").HasPrecision(18, 2);
            penalties.Property(p => p.AllowedDelayHours).HasColumnName("AllowedDelayHours").HasPrecision(5, 2);
            penalties.Property(p => p.MaintenancePenalty).HasColumnName("MaintenancePenalty").HasPrecision(18, 2);
            penalties.Property(p => p.AccidentPenalty).HasColumnName("AccidentPenalty").HasPrecision(18, 2);
            penalties.Property(p => p.AmountOfKmExceedingTheLimit).HasColumnName("AmountOfKmExceedingTheLimit").HasPrecision(18, 2);
        });

    }
}
