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

        builder.Property(x => x.AccountingNo)
            .HasMaxLength(50);

        builder.Property(x => x.ReferenceNo)
            .HasMaxLength(50);

        builder.Property(x => x.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.DriverName)
            .HasMaxLength(150);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        // Relationships with Company & Branch
        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        // Value Objects Configuration (Owned Entities)
        builder.OwnsOne(x => x.Period, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            period.Property(p => p.StartTime).HasColumnName("StartTime").IsRequired();
            period.Property(p => p.StartDay).HasColumnName("StartDay").HasMaxLength(20);
            period.Property(p => p.ExpectedReceivingDate).HasColumnName("ExpectedReceivingDate").IsRequired();
            period.Property(p => p.ExpectedReceivingTime).HasColumnName("ExpectedReceivingTime").IsRequired();
            period.Property(p => p.DeliveryDay).HasColumnName("DeliveryDay").HasMaxLength(20);
            period.Property(p => p.PeriodInDays).HasColumnName("PeriodInDays").IsRequired();
            period.Property(p => p.ActualPeriodInDays).HasColumnName("ActualPeriodInDays").IsRequired();
        });

        builder.OwnsOne(x => x.Tenant, tenant =>
        {
            tenant.Property(t => t.TenantName).HasColumnName("TenantName").HasMaxLength(200).IsRequired();
            tenant.Property(t => t.LicenseNumber).HasColumnName("TenantLicenseNumber").HasMaxLength(50).IsRequired();
            tenant.Property(t => t.PassportNumber).HasColumnName("TenantPassportNumber").HasMaxLength(50);
            tenant.Property(t => t.UnifiedNumber).HasColumnName("TenantUnifiedNumber").HasMaxLength(50);
            tenant.Property(t => t.IdNumber).HasColumnName("TenantIdNumber").HasMaxLength(50).IsRequired();
            tenant.Property(t => t.Mobile).HasColumnName("TenantMobile").HasMaxLength(25).IsRequired();
            tenant.Property(t => t.TenantBirthday).HasColumnName("TenantBirthday");
            tenant.Property(t => t.Age).HasColumnName("TenantAge");
        });

        builder.OwnsOne(x => x.Sponsor, sponsor =>
        {
            sponsor.Property(s => s.SponsorName).HasColumnName("SponsorName").HasMaxLength(200);
            sponsor.Property(s => s.Nationality).HasColumnName("SponsorNationality").HasMaxLength(100);
            sponsor.Property(s => s.LicenseNumber).HasColumnName("SponsorLicenseNumber").HasMaxLength(50);
            sponsor.Property(s => s.LicenseExpireDate).HasColumnName("SponsorLicenseExpireDate");
            sponsor.Property(s => s.IdNumber).HasColumnName("SponsorIdNumber").HasMaxLength(50);
            sponsor.Property(s => s.IdExpireDate).HasColumnName("SponsorIdExpireDate");
        });

        builder.OwnsOne(x => x.SecondDriver, driver =>
        {
            driver.Property(d => d.SecondDriverName).HasColumnName("SecondDriverName").HasMaxLength(200);
            driver.Property(d => d.Nationality).HasColumnName("SecondDriverNationality").HasMaxLength(100);
            driver.Property(d => d.LicenseNumber).HasColumnName("SecondDriverLicenseNumber").HasMaxLength(50);
            driver.Property(d => d.LicenseExpireDate).HasColumnName("SecondDriverLicenseExpireDate");
            driver.Property(d => d.IdNumber).HasColumnName("SecondDriverIdNumber").HasMaxLength(50);
            driver.Property(d => d.IdExpireDate).HasColumnName("SecondDriverIdExpireDate");
        });

        builder.OwnsOne(x => x.Vehicle, vehicle =>
        {
            vehicle.Property(v => v.PlateNo).HasColumnName("VehiclePlateNo").HasMaxLength(50).IsRequired();
            vehicle.Property(v => v.ModelYear).HasColumnName("VehicleModelYear").HasMaxLength(10);
            vehicle.Property(v => v.FileNo).HasColumnName("VehicleFileNo").HasMaxLength(50);
            vehicle.Property(v => v.StartKilometerCounter).HasColumnName("StartKilometerCounter").HasPrecision(18, 2);
            vehicle.Property(v => v.ReturnKilometerCounter).HasColumnName("ReturnKilometerCounter").HasPrecision(18, 2);
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

        builder.OwnsOne(x => x.DriverTerms, driverTerms =>
        {
            driverTerms.Property(d => d.DriverFare).HasColumnName("DriverFare").HasPrecision(18, 2);
            driverTerms.Property(d => d.DriverWorkingHoursPerDay).HasColumnName("DriverWorkingHoursPerDay").HasPrecision(5, 2);
            driverTerms.Property(d => d.DriverOvertimeAmountPerHour).HasColumnName("DriverOvertimeAmountPerHour").HasPrecision(18, 2);
            driverTerms.Property(d => d.DailyRate).HasColumnName("DriverDailyRate").HasPrecision(18, 2);
        });

        builder.OwnsOne(x => x.Mileage, mileage =>
        {
            mileage.Property(m => m.KilometerPerDay).HasColumnName("KilometerPerDay").HasPrecision(18, 2);
            mileage.Property(m => m.MaximumKilometerPerDay).HasColumnName("MaximumKilometerPerDay").HasPrecision(18, 2);
            mileage.Property(m => m.AmountOfKmExceedingLimit).HasColumnName("AmountOfKmExceedingLimit").HasPrecision(18, 2);
        });

        builder.OwnsOne(x => x.Maintenance, maintenance =>
        {
            maintenance.Property(m => m.NextMaintenanceDate).HasColumnName("NextMaintenanceDate");
            maintenance.Property(m => m.NextMaintenanceKm).HasColumnName("NextMaintenanceKm").HasPrecision(18, 2);
            maintenance.Property(m => m.ReminderBeforePeriodicMaintenance).HasColumnName("ReminderBeforePeriodicMaintenance");
            maintenance.Property(m => m.NotificationType).HasColumnName("NotificationType");
        });
    }
}

