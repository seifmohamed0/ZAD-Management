using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Infrastructure.Persistence.Seed;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();

        try
        {
            await SeedCompaniesAndBranchesAsync(context);
            await SeedRentalContractsAsync(context);
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    private static async Task SeedCompaniesAndBranchesAsync(ApplicationDbContext context)
    {
        if (await context.Companies.AnyAsync())
            return;

        var company = new Company
        {
            Code = "ZAD-CO-01",
            ArabicName = "شركة زاد لتأجير السيارات",
            EnglishName = "ZAD Vehicle Rental Co.",
            ArabicAddress = "طريق الملك فهد، الرياض",
            EnglishAddress = "King Fahd Road, Riyadh",
            Country = "Saudi Arabia",
            City = "Riyadh",
            Language = "ar",
            Phone = "+966 11 123 4567",
            Website = "https://zad-rental.com",
            Logo = "zad_logo.png",
            IsActive = true
        };

        var mainBranch = new Branch
        {
            Company = company,
            Code = "RUH-01",
            ArabicName = "فرع الرياض الرئيسي",
            EnglishName = "Riyadh Main Branch",
            ArabicAddress = "حي السليمانية، الرياض",
            EnglishAddress = "Al Sulaimaniyah, Riyadh",
            Phone = "+966 11 987 6543",
            Logo = "branch_main.png",
            IsActive = true
        };

        var jeddahBranch = new Branch
        {
            Company = company,
            Code = "JED-01",
            ArabicName = "فرع مطار الملك عبدالعزيز - جدة",
            EnglishName = "Jeddah Airport Branch",
            ArabicAddress = "مطار الملك عبدالعزيز الدولي، جدة",
            EnglishAddress = "King Abdulaziz Int. Airport, Jeddah",
            Phone = "+966 12 654 3210",
            Logo = "branch_jeddah.png",
            IsActive = true
        };

        await context.Companies.AddAsync(company);
        await context.Branches.AddRangeAsync(mainBranch, jeddahBranch);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRentalContractsAsync(ApplicationDbContext context)
    {
        if (await context.RentalContracts.AnyAsync())
            return;

        var company = await context.Companies.FirstOrDefaultAsync();
        var branch = await context.Branches.FirstOrDefaultAsync();

        if (company == null || branch == null)
            return;

        // Contract 1: Active Standard Rental
        var period1 = new ContractPeriod(
            DateTime.UtcNow.Date.AddDays(-2),
            new TimeSpan(10, 0, 0),
            DateTime.UtcNow.Date.AddDays(3),
            new TimeSpan(10, 0, 0),
            5
        );

        var tenant1 = new TenantSnapshot(
            "محمد عبد الرحمن الشمري",
            "LIC-998822",
            "1088776655",
            "0551234567",
            "P889900",
            "7009876543",
            new DateTime(1994, 7, 15)
        );

        var sponsor1 = new SponsorSnapshot(
            "شركة الأفق للتجارة",
            "Saudi",
            "LIC-001122",
            DateTime.UtcNow.AddYears(2),
            "1022334455",
            DateTime.UtcNow.AddYears(3)
        );

        var secondDriver1 = new DriverSnapshot(
            "فهد ناصر القحطاني",
            "Saudi",
            "LIC-554433",
            DateTime.UtcNow.AddYears(1),
            "1077665544",
            DateTime.UtcNow.AddYears(2)
        );

        var vehicle1 = new RentedVehicleSnapshot(
            "أ ب ج 1234",
            "2024",
            "FILE-2024-001",
            24500.0m
        );

        var pricing1 = new RentalPricing(250.0m, 10.0m, 25.0m);
        var penalties1 = new PenaltyPolicy(50.0m, 2.0m, 150.0m, 500.0m);
        var driverTerms1 = new PrivateDriverTerms(100.0m, 8.0m, 25.0m, 120.0m);
        var mileage1 = new MileagePolicy(200.0m, 350.0m, 1.5m);
        var maintenance1 = new MaintenanceAlert(DateTime.UtcNow.AddMonths(2), 30000.0m, 7, NotificationType.Kilometer);

        var contract1 = new RentalContract(
            company.Id,
            branch.Id,
            "RC-202608-0001",
            "ACC-10024",
            "REF-8891",
            "SAR",
            ContractType.Daily,
            PaymentType.CreditCard,
            false,
            null,
            "عقد إيجار يومي - سيارة بحالة ممتازة",
            period1,
            tenant1,
            sponsor1,
            secondDriver1,
            vehicle1,
            pricing1,
            penalties1,
            driverTerms1,
            mileage1,
            maintenance1
        );
        contract1.Activate();

        // Contract 2: Draft VIP Rental with Driver
        var period2 = new ContractPeriod(
            DateTime.UtcNow.Date,
            new TimeSpan(14, 30, 0),
            DateTime.UtcNow.Date.AddDays(7),
            new TimeSpan(14, 30, 0),
            7
        );

        var tenant2 = new TenantSnapshot(
            "سلطان بن عبدالعزيز المنصور",
            "LIC-334455",
            "1044556677",
            "0509876543",
            "P112233",
            null,
            new DateTime(1988, 3, 20)
        );

        var vehicle2 = new RentedVehicleSnapshot(
            "د هـ و 9999",
            "2025",
            "FILE-VIP-009",
            5200.0m
        );

        var pricing2 = new RentalPricing(600.0m, 0m, 0m);
        var penalties2 = new PenaltyPolicy(100.0m, 1.0m, 300.0m, 1000.0m);
        var driverTerms2 = new PrivateDriverTerms(200.0m, 10.0m, 40.0m, 250.0m);
        var mileage2 = new MileagePolicy(300.0m, 500.0m, 2.0m);
        var maintenance2 = new MaintenanceAlert(DateTime.UtcNow.AddMonths(4), 15000.0m, 7, NotificationType.Both);

        var contract2 = new RentalContract(
            company.Id,
            branch.Id,
            "RC-202608-0002",
            "ACC-10025",
            "REF-9942",
            "SAR",
            ContractType.Weekly,
            PaymentType.BankTransfer,
            true,
            "سائق خاص: عبدالله السعيد",
            "عقد VIP أسبوعي مع سائق خاص",
            period2,
            tenant2,
            null,
            null,
            vehicle2,
            pricing2,
            penalties2,
            driverTerms2,
            mileage2,
            maintenance2
        );

        await context.RentalContracts.AddRangeAsync(contract1, contract2);
        await context.SaveChangesAsync();
    }
}
