using ZAD_Management.Domain.Entities;
using ZAD_Management.Domain.Enums;
using ZAD_Management.Domain.Services.Calculations;
using ZAD_Management.Domain.ValueObjects;
using Xunit;

namespace ZAD_Management.Domain.Tests;

public class RentalSettlementCalculatorTests
{
    [Fact]
    public void Calculate_ShouldCombineRentPenaltiesMileageAndPayments()
    {
        var contract = CreateContract();
        var input = new RentalSettlementInput(
            new DateTime(2026, 1, 4, 10, 0, 0),
            1450m,
            40m,
            30m,
            50m,
            400m,
            25m,
            10m,
            false);

        var result = new RentalSettlementCalculator().Calculate(contract, input);

        Assert.Equal(3m, result.ActualPeriodInDays);
        Assert.Equal(600m, result.BaseRent);
        Assert.Equal(60m, result.DiscountAmount);
        Assert.Equal(1100m, result.DelayPenalty);
        Assert.Equal(300m, result.ExceededKilometersAmount);
        Assert.Equal(40m, result.MaintenancePenaltyAmount);
        Assert.Equal(2050m, result.TotalAmount);
        Assert.Equal(1625m, result.NetDueAmount);
    }

    [Fact]
    public void Calculate_ShouldRejectNegativeSettlementAmountsAndLowerReturnCounter()
    {
        var contract = CreateContract();
        var calculator = new RentalSettlementCalculator();

        var negativePayment = new RentalSettlementInput(
            new DateTime(2026, 1, 4, 10, 0, 0),
            1450m,
            0m,
            0m,
            0m,
            -1m,
            0m,
            0m,
            false);
        var lowerCounter = negativePayment with { PaidAmount = 0m, ReturnKilometerCounter = 900m };

        Assert.Throws<ArgumentException>(() => calculator.Calculate(contract, negativePayment));
        Assert.Throws<ArgumentException>(() => calculator.Calculate(contract, lowerCounter));
    }

    private static RentalContract CreateContract()
    {
        var period = new ContractPeriod(
            new DateTime(2026, 1, 1),
            new TimeSpan(10, 0, 0),
            new DateTime(2026, 1, 3),
            new TimeSpan(10, 0, 0),
            2);
        var tenant = new TenantSnapshot("Tenant", "LIC-1", "ID-1", "01000000000");
        var vehicle = new RentedVehicleSnapshot("ABC-123", "2024", 1000m, 100m, 100m);
        var pricing = new RentalPricing(200m, 10m);
        var penalties = new PenaltyPolicy(50m, 2m, 150m, 500m, 2m);

        return new RentalContract(
            1,
            1,
            "RC-TEST",
            ContractType.Daily,
            false,
            period,
            tenant,
            null,
            vehicle,
            pricing,
            penalties);
    }
}
