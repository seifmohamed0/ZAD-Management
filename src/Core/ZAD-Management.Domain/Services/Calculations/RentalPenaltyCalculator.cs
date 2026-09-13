using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Domain.Services.Calculations;

public static class RentalPenaltyCalculator
{
    public static decimal CalculateDelayPenalty(
        DateTime expectedReturn,
        DateTime actualReturn,
        PenaltyPolicy penalties)
    {
        var billableHours = GetBillableDelayHours(expectedReturn, actualReturn, penalties);

        return Math.Ceiling(billableHours) * penalties.DelayPenaltyPerHour;
    }

    public static decimal GetBillableDelayHours(
        DateTime expectedReturn,
        DateTime actualReturn,
        PenaltyPolicy penalties)
    {
        var delayedHours = Math.Max(0m, (decimal)(actualReturn - expectedReturn).TotalHours);
        return Math.Max(0m, delayedHours - penalties.AllowedDelayHours);
    }
}
