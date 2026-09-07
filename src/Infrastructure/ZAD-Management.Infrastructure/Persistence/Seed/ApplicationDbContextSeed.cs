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
            ArabicAddress = "القاهرة",
            EnglishAddress = "Cairo",
            Country = "Egypt",
            City = "cairo",
            Language = "ar",
            Phone = "+20 1025729182",
            Website = "https://zad-rental.com",
            Logo = "zad_logo.png",
            IsActive = true
        };

        var mainBranch = new Branch
        {
            Company = company,
            Code = "CAI-01",
            ArabicName = "فرع القاهرة الرئيسي",
            EnglishName = "Cairo Main Branch",
            ArabicAddress = "القاهرة",
            EnglishAddress = "Cairo",
            Phone = "+20 1025729182",
            Logo = "branch_main.png",
            IsActive = true
        };

        var alexBrach = new Branch
        {
            Company = company,
            Code = "ALEX-01",
            ArabicName = "فرع الإسكندرية",
            EnglishName = "Alexandria Branch",
            ArabicAddress = "الإسكندرية",
            EnglishAddress = "Alexandria",
            Phone = "+20 3 123 4567",
            Logo = "branch_alexandria.png",
            IsActive = true
        };

        await context.Companies.AddAsync(company);
        await context.Branches.AddRangeAsync(mainBranch, alexBrach);
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
            "سيف الدين محمد كمال",
            "LIC-998822",
            "1088776655",
            "0551234567"
        );

        var driver1 = new DriverSnapshot(
            "مازم محمد كمال",
            "Egyptian",
            "LIC-554433",
            DateTime.UtcNow.AddYears(1),
            "1077665544",
            DateTime.UtcNow.AddYears(2)
        );

        var vehicle1 = new RentedVehicleSnapshot(
            "أ ب ج 1234",
            "2024",
            24500.0m
        );

        var pricing1 = new RentalPricing(250.0m, 10.0m, 25.0m);
        var penalties1 = new PenaltyPolicy(50.0m, 2.0m, 150.0m, 500.0m);

        var contract1 = new RentalContract(
            company.Id,
            branch.Id,
            "RC-202608-0001",
            ContractType.Daily,
            false,
            period1,
            tenant1,
            driver1,
            vehicle1,
            pricing1,
            penalties1
        );

        var period2 = new ContractPeriod(
            DateTime.UtcNow.Date,
            new TimeSpan(14, 30, 0),
            DateTime.UtcNow.Date.AddDays(7),
            new TimeSpan(14, 30, 0),
            7
        );

        var tenant2 = new TenantSnapshot(
            "كريم محمد",
            "LIC-334455",
            "1044556677",
            "0509876543"
        );

        var vehicle2 = new RentedVehicleSnapshot(
            "د هـ و 9999",
            "2025",
            5200.0m
        );

        var pricing2 = new RentalPricing(600.0m, 0m, 0m);
        var penalties2 = new PenaltyPolicy(100.0m, 1.0m, 300.0m, 1000.0m);

        var contract2 = new RentalContract(
            company.Id,
            branch.Id,
            "RC-202608-0002",
            ContractType.Weekly,
            true,
            period2,
            tenant2,
            null,
            vehicle2,
            pricing2,
            penalties2
        );

        await context.RentalContracts.AddRangeAsync(contract1, contract2);
        await context.SaveChangesAsync();
    }
}
