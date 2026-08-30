namespace ZAD_Management.Domain.ValueObjects;

public class MileagePolicy
{
    public decimal KilometerPerDay { get; private set; }
    public decimal MaximumKilometerPerDay { get; private set; }
    public decimal AmountOfKmExceedingLimit { get; private set; }

    private MileagePolicy() { }

    public MileagePolicy(
        decimal kilometerPerDay,
        decimal maximumKilometerPerDay,
        decimal amountOfKmExceedingLimit)
    {
        KilometerPerDay = Math.Max(0, kilometerPerDay);
        MaximumKilometerPerDay = Math.Max(0, maximumKilometerPerDay);
        AmountOfKmExceedingLimit = Math.Max(0, amountOfKmExceedingLimit);
    }

    public decimal CalculateExcessMileageFee(decimal totalDrivenKm, int rentalDays)
    {
        decimal totalAllowedKm = KilometerPerDay * Math.Max(1, rentalDays);
        if (totalDrivenKm <= totalAllowedKm) return 0;

        decimal excessKm = totalDrivenKm - totalAllowedKm;
        return excessKm * AmountOfKmExceedingLimit;
    }
}

