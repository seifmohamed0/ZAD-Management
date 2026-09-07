using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Infrastructure.Persistence.Configurations;

public class RentalContractConfiguration : IEntityTypeConfiguration<RentalContract>
{
    public void Configure(EntityTypeBuilder<RentalContract> builder)
    {
        builder.ToTable("RentalContracts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContractNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FinalBaseRent)
            .HasPrecision(18, 2);

        builder.Property(x => x.FinalDiscount)
            .HasPrecision(18, 2);

        builder.Property(x => x.FinalDelayPenalty)
            .HasPrecision(18, 2);

        builder.Property(x => x.FinalTotalAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.ActualReturnKm)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

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
            vehicle.Property(v => v.KilometerCounter).HasColumnName("StartKilometerCounter").HasPrecision(18, 2);
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
        });
    }
}
